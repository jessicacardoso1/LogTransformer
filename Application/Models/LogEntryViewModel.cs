using LogTransformer.Core.Entities;

namespace LogTransformer.Application.Models
{
    public class LogEntryViewModel
    {
        public LogEntryViewModel(int id, string originalContent)
        {
            Id = id;
            OriginalContent = originalContent;
        }
        public int Id { get; private set; }
        public string OriginalContent { get; private set; }

        public static LogEntryViewModel FromEntity(LogEntry logEntry) {
            return new LogEntryViewModel(logEntry.Id,logEntry.OriginalContent);
        }
    }
}
