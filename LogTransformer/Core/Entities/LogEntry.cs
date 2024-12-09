using DevFreela.Core.Entities;
using System;

namespace LogTransformer.Core.Entities
{
    public class LogEntry : BaseEntity
    {
        public LogEntry(string originalContent)
        {
            OriginalContent = originalContent;
        }

        public string OriginalContent { get; set; }
    }

}
