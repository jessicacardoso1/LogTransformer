using DevFreela.Core.Entities;
using System;

namespace LogTransformer.Core.Entities
{
    public class LogEntry : BaseEntity
    {
        public string Provider { get; set; } = "MINHA CDN";
        public string HttpMethod { get; set; }
        public int StatusCode { get; set; }
        public string UriPath { get; set; }
        public double TimeTaken { get; set; }
        public int ResponseSize { get; set; }
        public string CacheStatus { get; set; }
    }

}
