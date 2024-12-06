using LogTransformer.Application.Models;
using LogTransformer.Core.Entities;
using MediatR;

namespace LogTransformer.Application.Commands.InsertLogEntry
{
    public class InsertLogEntryCommand : IRequest<ResultViewModel<int>>
    {
        public string OriginalContent { get; private set; }
        public string SourceFileName { get; private set; }

        public LogEntry ToEntity()
        {
            return new LogEntry(OriginalContent, SourceFileName);
        }
    }
}
