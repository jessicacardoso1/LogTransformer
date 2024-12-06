using LogTransformer.Application.Commands.TransformLog;
using LogTransformer.Application.Models;
using LogTransformer.Core.Entities;
using LogTransformer.Core.Repositories;
using LogTransformer.Infrastructure.Persistence.Repositories;
using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
            var transformedLog = await _tranformedLogRepository.GetTransformedLogByLogIdAsync(request.LogId.Value);

            if (transformedLog != null)
            {
                return HandleExistingTransformedLog(transformedLog, request.OutputFormat);
            }

            var logEntry = await _logEntryRepository.GetLogByIdAsync(request.LogId.Value);
            if (logEntry == null)
            {
                return ResultViewModel<string>.Error("Log não encontrado.");
            }

            var logLines = logEntry.OriginalContent.Split('\n').ToList();
            return await TransformAndSaveLogAsync(logLines, logEntry, request.OutputFormat);
        }

        private async Task<ResultViewModel<string>> HandleLogByUrlAsync(TransformLogCommand request, CancellationToken cancellationToken)
        {
            var logContent = await _fileService.DownloadLogAsync(request.LogUrl);

            if (string.IsNullOrEmpty(logContent))
            {
                return ResultViewModel<string>.Error("O conteúdo do log está vazio.");
            }

            var logLines = JsonConvert.DeserializeObject<List<string>>(logContent);
            var logEntry = new LogEntry(logContent, request.LogUrl);
            await _logEntryRepository.SaveLogAsync(logEntry);

            return await TransformAndSaveLogAsync(logLines, logEntry, request.OutputFormat);
        }

        private async Task<ResultViewModel<string>> TransformAndSaveLogAsync(List<string> logLines, LogEntry logEntry, string outputFormat)
        {
            var transformedContent = new StringBuilder()
                .AppendLine("#Version: 1.0")
                .AppendLine($"#Date: {DateTime.Now:dd/MM/yyyy HH:mm:ss}")
                .AppendLine("#Fields: provider http-method status-code uri-path time-taken response-size cache-status");

            foreach (var line in logLines)
            {
                var transformedLine = _tranformedLogRepository.Transform(line);
                transformedContent.AppendLine(transformedLine);
            }

            var finalTransformedContent = transformedContent.ToString();
            var transformedLog = new TransformedLog(finalTransformedContent, logEntry.Id);

            var filePath = await _fileService.SaveLogToFileAsync(transformedLog);
            transformedLog.SetFilePath(filePath);
            await _logEntryRepository.SaveTransformedLogAsync(transformedLog);

            return outputFormat == "file"
                ? ResultViewModel<string>.Success(filePath)
                : ResultViewModel<string>.Success(finalTransformedContent);
        }

        private ResultViewModel<string> HandleExistingTransformedLog(TransformedLog transformedLog, string outputFormat)
        {
            if (outputFormat == "file" && _fileService.FileExists(transformedLog.FilePath))
            {
                return ResultViewModel<string>.Success(transformedLog.FilePath);
            }
            else if (outputFormat != "file")
            {
                return ResultViewModel<string>.Success(transformedLog.TransformedContent);
            }

            return ResultViewModel<string>.Error("Arquivo transformado não encontrado.");
        }
    }
}