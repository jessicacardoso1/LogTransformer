using LogTransformer.Core.Entities;
using System.Text;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace LogTransformer.Application.Models
{
    public class TransformedLogViewModel
    {
        public TransformedLogViewModel(int id, string provider, string httpMethod, int statusCode, string uriPath, int timeTaken, int responseSize, string cacheStatus, string filePath, int originalLogId)
        {
            Id = id;
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

        public int Id { get; private set; }
        public string Provider { get; set; } = string.Empty;
        public string HttpMethod { get; set; } = string.Empty;
        public int StatusCode { get; set; }
        public string UriPath { get; set; } = string.Empty;
        public int TimeTaken { get; set; }
        public int ResponseSize { get; set; }
        public string CacheStatus { get; set; } = string.Empty;
        public string FilePath { get; private set; }
        public LogEntry OriginalLog { get; private set; }
        public int OriginalLogId { get; private set; }

        public static TransformedLogViewModel FromEntity(TransformedLog transformedLog)
        {
            return new TransformedLogViewModel(transformedLog.Id, transformedLog.Provider, transformedLog.HttpMethod, transformedLog.StatusCode,transformedLog.UriPath, transformedLog.TimeTaken, transformedLog.ResponseSize, transformedLog.CacheStatus,transformedLog.FilePath, transformedLog.OriginalLogId);
        }

    }
}
