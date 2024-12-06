using LogTransformer.Core.Entities;
using LogTransformer.Core.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace LogTransformer.Infrastructure.Persistence.Repositories
{
    public class FileService : IFileService
    {
        private readonly HttpClient _httpClient;

        public FileService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<string> SaveLogToFileAsync(TransformedLog transformedLog)
        {
            var directoryPath = Path.Combine("Logs");
            Directory.CreateDirectory(directoryPath);

            var filePath = Path.Combine(directoryPath, $"log_{transformedLog.OriginalLogId}.txt");
            await File.WriteAllTextAsync(filePath, transformedLog.TransformedContent);

            return filePath;
        }

        public async Task<string> DownloadLogAsync(string logUrl)
        {
            try
            {
                var logContent = await _httpClient.GetStringAsync(logUrl);
                return logContent; 
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao baixar o log da URL: {logUrl}. Detalhes: {ex.Message}");
            }
        }
        public async Task<string> ReadFileAsync(string filePath)
        {
            if (!FileExists(filePath))
            {
                throw new FileNotFoundException($"Arquivo não encontrado: {filePath}");
            }

            return await File.ReadAllTextAsync(filePath);
        }
        public bool FileExists(string filePath)
        {
            return File.Exists(filePath);
        }
    }
}
