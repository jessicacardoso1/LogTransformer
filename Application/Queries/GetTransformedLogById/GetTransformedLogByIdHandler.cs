using LogTransformer.Application.Models;
using LogTransformer.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LogTransformer.Application.Queries.GetTransformedLogById
{
    public class GetTransformedLogByIdHandler : IRequestHandler<GetTransformedLogByIdQuery, ResultViewModel<TransformedLogViewModel>>
    {
        private readonly ITransformedLogRepository _repository;
        public GetTransformedLogByIdHandler(ITransformedLogRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel<TransformedLogViewModel>> Handle(GetTransformedLogByIdQuery request, CancellationToken cancellationToken)
        {
            var transformedLog = await _repository.GetTransformedLogByIdAsync(request.Id);

            if (transformedLog is null)
            {
                return ResultViewModel<TransformedLogViewModel>.Error("O Log Transformado não existe.");
            }

            var model = TransformedLogViewModel.FromEntity(transformedLog);

            return ResultViewModel<TransformedLogViewModel>.Success(model);
        }
    }
}
