using LogTransformer.Application.Models;
using MediatR;
using System.Collections.Generic;

namespace LogTransformer.Application.Queries.GetTransformedLogById
{
    public class GetTransformedLogByLogIdQuery : IRequest<ResultViewModel<List<TransformedLogViewModel>>>
    {
        public GetTransformedLogByLogIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
