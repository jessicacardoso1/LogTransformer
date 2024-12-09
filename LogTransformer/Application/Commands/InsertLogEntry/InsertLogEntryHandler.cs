using LogTransformer.Application.Models;
using LogTransformer.Core.Entities;
using LogTransformer.Core.Repositories;
using LogTransformer.Infrastructure.Persistence.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace LogTransformer.Application.Commands.InsertLogEntry
{
    public class InsertLogEntryHandler : IRequestHandler<InsertLogEntryCommand, ResultViewModel<int>>
    {
        private readonly ILogEntryRepository _repository;
        private readonly ITransformedLogRepository _transformedLogRepository;

        public InsertLogEntryHandler(ILogEntryRepository repository, ITransformedLogRepository transformedLogRepository)
        {
            _repository = repository;
            _transformedLogRepository = transformedLogRepository;

        }
        public async Task<ResultViewModel<int>> Handle(InsertLogEntryCommand request, CancellationToken cancellationToken)
        {
            var logEntry = request.ToEntity();
            await _repository.SaveLogAsync(logEntry);
 
            return ResultViewModel<int>.Success(logEntry.Id);
        }
    }
}
