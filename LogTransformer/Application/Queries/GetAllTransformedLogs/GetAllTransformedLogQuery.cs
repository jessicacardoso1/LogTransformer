using LogTransformer.Application.Models;
using MediatR;
using System.Collections.Generic;

namespace LogTransformer.Application.Queries.GetAllTransformedLogs
{
    public class GetAllTransformedLogQuery : IRequest<ResultViewModel<IEnumerable<TransformedLogViewModel>>>
    {
        public int Page { get; set; }
        public int Size { get; set; }

        public GetAllTransformedLogQuery(int page, int size)
        {
            Page = page;
            Size = size;
        }
    }
}
