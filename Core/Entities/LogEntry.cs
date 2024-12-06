using DevFreela.Core.Entities;
using System;

namespace LogTransformer.Core.Entities
{
    public class LogEntry : BaseEntity
    {
        public LogEntry(string originalContent, string sourceFileName = null)
        {
            OriginalContent = originalContent;
            SourceFileName = sourceFileName;
        }

        public string OriginalContent { get; private set; }
        public string SourceFileName { get; private set; }
    }

}
