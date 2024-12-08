using LogTransformer.Application.Models;
using MediatR;

namespace LogTransformer.Application.Queries.GetTransformedLogById
{
    public class GetTransformedLogByIdQuery : IRequest<ResultViewModel<TransformedLogViewModel>>
    {
        public GetTransformedLogByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
