using LogTransformer.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogTransformer.Application.Services
{
    public interface IFileService
    {
        Task<string> SaveLogToFileAsync(int OriginalLogId, string TransformedContent);
        Task<string> DownloadLogAsync(string logUrl);
        Task<string> ReadFileAsync(string filePath);
        bool FileExists(string filePath);

    }
}
