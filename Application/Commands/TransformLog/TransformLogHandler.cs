using LogTransformer.Application.Commands.TransformLog;
using LogTransformer.Application.Models;
using LogTransformer.Application.Services;
using LogTransformer.Core.Entities;
using LogTransformer.Core.Repositories;
using LogTransformer.Infrastructure.Persistence.Repositories;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LogTransformer.Application.Handlers
{
    public class TransformLogHandler : IRequestHandler<TransformLogCommand, ResultViewModel<string>>
    {
        private readonly ILogEntryRepository _logEntryRepository;
        private readonly ITransformedLogRepository _tranformedLogRepository;
        private readonly IFileService _fileService;

        public TransformLogHandler(ILogEntryRepository logRepository, ITransformedLogRepository logTransformer, IFileService fileService)
        {
            _logEntryRepository = logRepository;
            _tranformedLogRepository = logTransformer;
            _fileService = fileService;
        }


        public async Task<ResultViewModel<string>> Handle(TransformLogCommand request, CancellationToken cancellationToken)
        {
            List<string> logLines = new List<string>();
            LogEntry logEntry = null;
            TransformedLog transformedLog = null;

            if (request.LogId.HasValue)
            {
                return await HandleLogByIdAsync(request, cancellationToken);
            }
            else if (!string.IsNullOrEmpty(request.LogUrl))
            {
                return await HandleLogByUrlAsync(request, cancellationToken);
            }

            return ResultViewModel<string>.Error("Parâmetros inválidos.");
        }

        private async Task<ResultViewModel<string>> HandleLogByIdAsync(TransformLogCommand request, CancellationToken cancellationToken)
        {
            var transformedLog = await _tranformedLogRepository.GetTransformedLogsByLogIdAsync(request.LogId.Value);

            if (transformedLog.Count > 0)
            {
                return HandleExistingTransformedLog(transformedLog, request.OutputFormat);
            }

            var logEntry = await _logEntryRepository.GetLogByIdAsync(request.LogId.Value);
            if (logEntry == null)
            {
                return ResultViewModel<string>.Error("Log não encontrado.");
            }

            var logLines = logEntry.OriginalContent.Split('\n').ToList();
            return await TransformAndSaveLogAsync(logEntry, request.OutputFormat);
        }

        private async Task<ResultViewModel<string>> HandleLogByUrlAsync(TransformLogCommand request, CancellationToken cancellationToken)
        {
            var logContent = await _fileService.DownloadLogAsync(request.LogUrl);

            if (string.IsNullOrEmpty(logContent))
            {
                return ResultViewModel<string>.Error("O conteúdo do log está vazio.");
            }

            var logEntry = new LogEntry(logContent);
            await _logEntryRepository.SaveLogAsync(logEntry);

            return await TransformAndSaveLogAsync(logEntry, request.OutputFormat);
        }

        private async Task<ResultViewModel<string>> TransformAndSaveLogAsync(LogEntry logEntry, string outputFormat)
        {
            var transformedLogs = new List<TransformedLog>();
            var lines = logEntry.OriginalContent.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines)
            {
                var transformedLog = ProcessLine(line, logEntry.Id);
                if (transformedLog != null)
                {
                    await _tranformedLogRepository.SaveTransformedLogAsync(transformedLog);

                    transformedLogs.Add(transformedLog);
                }
            }

            var finalTransformedContent = GenerateTransformedLogFileContent(transformedLogs);
            var filePath = await _fileService.SaveLogToFileAsync(logEntry.Id, finalTransformedContent);

            return outputFormat == "file"
                ? ResultViewModel<string>.Success(filePath)
                : ResultViewModel<string>.Success(finalTransformedContent);
        }


        public string GenerateTransformedLogFileContent(IEnumerable<TransformedLog> transformedLogs)
        {
            var sb = new StringBuilder();

            sb.AppendLine("#Version: 1.0");
            sb.AppendLine($"#Date: {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
            sb.AppendLine("#Fields: provider http-method status-code uri-path time-taken response-size cache-status");

            foreach (var log in transformedLogs)
            {
                sb.AppendLine(
                    $"{log.Provider} {log.HttpMethod} {log.StatusCode} {log.UriPath} {log.TimeTaken} {log.ResponseSize} {log.CacheStatus}"
                );
            }

            return sb.ToString();
        }

        private TransformedLog ProcessLine(string line, int originalLogId)
        {
            try
            {
                var parts = line.Split('|');
                if (parts.Length != 5) return null;

                var httpInfo = parts[3].Replace("\"", "").Split(' ');

                return new TransformedLog(
                    httpMethod: httpInfo[0],
                    statusCode: int.Parse(parts[1]),
                    uriPath: httpInfo[1],
                    timeTaken: (int)Math.Round(double.Parse(parts[4], CultureInfo.InvariantCulture)),
                    responseSize: int.Parse(parts[0]),
                    cacheStatus: parts[2] == "INVALIDATE" ? "REFRESH_HIT" : parts[2],
                    filePath: "",
                    originalLogId: originalLogId,
                    provider: "MINHA CDN" 
                );

            }
            catch
            {
                return null;
            }
        }

        private ResultViewModel<string> HandleExistingTransformedLog(List<TransformedLog> transformedLogs, string outputFormat)
        {
            if ((outputFormat) == "file" && _fileService.FileExists(transformedLogs[0].FilePath))
            {
                return ResultViewModel<string>.Success(transformedLogs[0].FilePath);
            }
            else if (outputFormat != "file")
            {
                var finalTransformedContent = GenerateTransformedLogFileContent(transformedLogs);
                return ResultViewModel<string>.Success(finalTransformedContent);
            }

            return ResultViewModel<string>.Error("Arquivo transformado não encontrado.");
        }
    }
}