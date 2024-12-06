using LogTransformer.Application.Models;
using LogTransformer.Core.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace LogTransformer.Application.Commands.InsertLogEntry
{
    public class InsertLogEntryHandler : IRequestHandler<InsertLogEntryCommand, ResultViewModel<int>>
    {
        private readonly ILogEntryRepository _repository;
        public InsertLogEntryHandler(ILogEntryRepository repository)
        {
            _repository = repository;
        }
        public async Task<ResultViewModel<int>> Handle(InsertLogEntryCommand request, CancellationToken cancellationToken)
        {
            var logEntry = request.ToEntity();
            await _repository.AddAsync(logEntry);

            return ResultViewModel<int>.Success(logEntry.Id);
        }
    }
}
