using LogTransformer.Application.Models;
using MediatR;

namespace LogTransformer.Application.Commands.TransformLog
{
    public class TransformLogCommand : IRequest<ResultViewModel<string>>
    {
        public TransformLogCommand(string logUrl, int? logId, string outputFormat)
        {
            LogUrl = logUrl;
            LogId = logId;
            OutputFormat = outputFormat;
        }

        public string LogUrl { get; set; }
        public int? LogId { get; set; }
        public string OutputFormat { get; set; }
    }

}

