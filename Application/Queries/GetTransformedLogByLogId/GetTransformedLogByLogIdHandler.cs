using LogTransformer.Application.Models;
using LogTransformer.Core.Entities;
using LogTransformer.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LogTransformer.Application.Queries.GetTransformedLogById
{
    public class GetTransformedLogByLogIdHandler : IRequestHandler<GetTransformedLogByLogIdQuery, ResultViewModel<List<TransformedLogViewModel>>>
    {
        private readonly ITransformedLogRepository _repository;
        public GetTransformedLogByLogIdHandler(ITransformedLogRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResultViewModel<List<TransformedLogViewModel>>> Handle(GetTransformedLogByLogIdQuery request, CancellationToken cancellationToken)
        {
            var transformedLog = await _repository.GetTransformedLogsByLogIdAsync(request.Id);

            if (transformedLog is null)
            {
                return ResultViewModel<List<TransformedLogViewModel>>.Error("O Log Transformado não existe.");
            }

            var model = transformedLog.Select(TransformedLogViewModel.FromEntity).ToList();


            return ResultViewModel<List<TransformedLogViewModel>>.Success(model);
        }
    }
}
