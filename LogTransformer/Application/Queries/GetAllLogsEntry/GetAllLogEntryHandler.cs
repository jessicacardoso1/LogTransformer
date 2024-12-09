using LogTransformer.Application.Models;
using LogTransformer.Core.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LogTransformer.Application.Queries.GetAllLogsEntry
{
    public class GetAllLogEntryHandler : IRequestHandler<GetAllLogsEntryQuery, ResultViewModel<IEnumerable<LogEntryViewModel>>>
    {
        private readonly ILogEntryRepository _repository;
        public GetAllLogEntryHandler(ILogEntryRepository repository)
        {
            _repository = repository;
        }
        public async Task<ResultViewModel<IEnumerable<LogEntryViewModel>>> Handle(GetAllLogsEntryQuery request, CancellationToken cancellationToken)
        {
            var logEntry = await _repository.GetAllLogsAsync(request.Page, request.Size);

            var model = logEntry.Select(LogEntryViewModel.FromEntity).ToList();

            return ResultViewModel<IEnumerable<LogEntryViewModel>>.Success(model);

        }
    }
}
