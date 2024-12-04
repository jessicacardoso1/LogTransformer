using DevFreela.Core.Entities;
using System;

namespace LogTransformer.Core.Entities
{
    public class TransformedLog : BaseEntity
    {
        public string TransformedContent { get; set; }
        public int OriginalLogId { get; set; }
        public LogEntry OriginalLog { get; set; }
    }

}
