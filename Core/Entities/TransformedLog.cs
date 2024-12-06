using DevFreela.Core.Entities;
using System;

namespace LogTransformer.Core.Entities
{
    public class TransformedLog : BaseEntity
    {
        public TransformedLog(string transformedContent, int originalLogId, string filePath = null)
        {
            TransformedContent = transformedContent;
            OriginalLogId = originalLogId;
            FilePath = filePath;
        }

        public string TransformedContent { get; private set; }
        public int OriginalLogId { get; private set; }
        public LogEntry OriginalLog { get; private set; }
        public string FilePath { get; private set; }
        public void SetFilePath(string filePath)
        {
            FilePath = filePath;
        }
    }
}
