using LogTransformer.Application.Commands.InsertLogEntry;
using LogTransformer.Application.Queries.GetAllLogsEntry;
using LogTransformer.Application.Commands.TransformLog;
using LogTransformer.Application.Queries.GetAllTransformedLogs;
using LogTransformer.Application.Queries.GetLogEntryById;
using LogTransformer.Application.Queries.GetTransformedLogById;
using LogTransformer.Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MediatR;
using LogTransformer.Application.Models;

namespace LogTransformer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LogsController(ILogEntryRepository logEntryRepository, ITransformedLogRepository transformedLogRepository, IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("saved")]
        public async Task<IActionResult> GetSavedLogs(int page = 0, int size = 3)
        {
            var result = await _mediator.Send(new GetAllLogsEntryQuery(page, size));
            return Ok(result);
        }

        [HttpGet("transformed")]
        public async Task<IActionResult> GetTransformedLogs(int page = 0, int size = 3)
        {
            var result = await _mediator.Send(new GetAllTransformedLogQuery(page, size));
            return Ok(result);
        }

        [HttpGet("saved/{id}")]
        public async Task<IActionResult> GetSavedLogById(int id)
        {
            var result = await _mediator.Send(new GetLogEntryByIdQuery(id));
            return HandleResult(result);
        }

        [HttpGet("transformed/{id}")]
        public async Task<IActionResult> GetTransformedLogById(int id)
        {
            var result = await _mediator.Send(new GetTransformedLogByIdQuery(id));
            return HandleResult(result);
        }

        [HttpGet("transformedByLogId/{id}")]
        public async Task<IActionResult> GetTransformedLogByLogId(int id)
        {
            var result = await _mediator.Send(new GetTransformedLogByLogIdQuery(id));
            return HandleResult(result);
        }


        [HttpPost("save")]
        public async Task<IActionResult> SaveLog([FromBody] InsertLogEntryCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return CreatedAtAction(nameof(GetTransformedLogById), new { id = result.Data }, command);
        }

        [HttpPost("transform")]
        [Produces("text/plain")] // Define que a resposta será do tipo texto simples
        public async Task<IActionResult> TransformLog([FromBody] TransformLogCommand command)
        {
            var result = await _mediator.Send(command);

            // Verifica se a transformação foi bem-sucedida
            if (result.IsSuccess)
            {
                // Retorna o conteúdo transformado em texto simples
                return Content(result.Data, "text/plain");
            }

            return HandleResult(result);
        }
        private IActionResult HandleResult<T>(ResultViewModel<T> result)
        {
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
    }
}
