using LogTransformer.Application.Models;
using LogTransformer.Application.Queries.GetAllLogsEntry;
using LogTransformer.Core.Repositories;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace LogTransformer.Application.Queries.GetLogEntryById
{
    public class GetLogEntryByIdHandler : IRequestHandler<GetLogEntryByIdQuery, ResultViewModel<LogEntryViewModel>>
    {
        private readonly ILogEntryRepository _repository;
        public GetLogEntryByIdHandler(ILogEntryRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel<LogEntryViewModel>> Handle(GetLogEntryByIdQuery request, CancellationToken cancellationToken)
        {
            var logEntry = await _repository.GetLogByIdAsync(request.Id);

            if (logEntry is null)
            {
                return ResultViewModel<LogEntryViewModel>.Error("O Log Transformado não existe.");
            }

            var model = LogEntryViewModel.FromEntity(logEntry);

            return ResultViewModel<LogEntryViewModel>.Success(model);
        }
    }

}
