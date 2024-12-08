using LogTransformer.Application.Models;
using MediatR;
using System.Collections.Generic;

namespace LogTransformer.Application.Queries.GetAllTransformedLogs
{
    public class GetAllTransformedLogQuery : IRequest<ResultViewModel<IEnumerable<TransformedLogViewModel>>>
    {
    }
}
