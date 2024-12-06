using LogTransformer.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LogTransformer.Core.Repositories
{
    public interface IFileService
    {
        Task<string> SaveLogToFileAsync(TransformedLog transformedLog);
        Task<string> DownloadLogAsync(string logUrl);
        Task<string> ReadFileAsync(string filePath);
        bool FileExists(string filePath);

    }
}
