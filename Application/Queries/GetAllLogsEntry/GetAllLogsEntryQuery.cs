using LogTransformer.Application.Models;
using MediatR;
using System.Collections.Generic;

namespace LogTransformer.Application.Queries.GetAllLogsEntry
{
    public class GetAllLogsEntryQuery : IRequest<ResultViewModel<IEnumerable<LogEntryViewModel>>>
    {
        public GetAllLogsEntryQuery(string query)
        {
            Query = query;
        }

        public string Query { get; private set; }
    }
}
