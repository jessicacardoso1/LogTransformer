using LogTransformer.Application.Models;
using LogTransformer.Core.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LogTransformer.Application.Queries.GetAllTransformedLogs
{
    public class GetAllTransformedLogHandler : IRequestHandler<GetAllTransformedLogQuery, ResultViewModel<IEnumerable<TransformedLogViewModel>>>
    {
        private readonly ITransformedLogRepository _repository;
        public GetAllTransformedLogHandler(ITransformedLogRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel<IEnumerable<TransformedLogViewModel>>> Handle(GetAllTransformedLogQuery request, CancellationToken cancellationToken)
        {
            var TransformedLogs = await _repository.GetAllTransformedLogsAsync(request.Page, request.Size);

            var model = TransformedLogs.Select(TransformedLogViewModel.FromEntity).ToList();

            return ResultViewModel<IEnumerable<TransformedLogViewModel>>.Success(model);
        }
    }
}
