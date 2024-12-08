using LogTransformer.Application.Models;
using MediatR;

namespace LogTransformer.Application.Queries.GetLogEntryById
{
    public class GetLogEntryByIdQuery : IRequest<ResultViewModel<LogEntryViewModel>>
    {
        public GetLogEntryByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
