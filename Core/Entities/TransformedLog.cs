using DevFreela.Core.Entities;
using System;

namespace LogTransformer.Core.Entities
{
    public class TransformedLog : BaseEntity
    {

        public TransformedLog(string httpMethod, int statusCode, string uriPath, int timeTaken, int responseSize, string cacheStatus, string filePath, int originalLogId, string provider = "MINHA CDN")
        {
            Provider = provider;
            HttpMethod = httpMethod;
            StatusCode = statusCode;
            UriPath = uriPath;
            TimeTaken = timeTaken;
            ResponseSize = responseSize;
            CacheStatus = cacheStatus;
            FilePath = filePath;
            OriginalLogId = originalLogId;
        }


        public string Provider { get; set; } = string.Empty; 
        public string HttpMethod { get; set; } = string.Empty; 
        public int StatusCode { get; set; }
        public string UriPath { get; set; } = string.Empty; 
        public int TimeTaken { get; set; } 
        public int ResponseSize { get; set; } 
        public string CacheStatus { get; set; } = string.Empty;
        public string FilePath { get; set; }
        public int OriginalLogId { get; set; }
        public LogEntry OriginalLog { get; set; }
        public void SetFilePath(string filePath)
        {
            FilePath = filePath;
        }
    }
}
