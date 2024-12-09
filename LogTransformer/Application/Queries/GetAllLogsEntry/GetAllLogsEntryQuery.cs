using LogTransformer.Application.Models;
using MediatR;
using System.Collections.Generic;

namespace LogTransformer.Application.Queries.GetAllLogsEntry
{
    public class GetAllLogsEntryQuery : IRequest<ResultViewModel<IEnumerable<LogEntryViewModel>>>
    {
        public int Page { get; set; }
        public int Size { get; set; }

        public GetAllLogsEntryQuery(int page, int size)
        {
            Page = page;
            Size = size;
        }

    }
}
