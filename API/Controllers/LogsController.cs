using DevFreela.Core.Entities;
using LogTransformer.Core.Entities;
using LogTransformer.Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MediatR;
using LogTransformer.Application.Commands.InsertLogEntry;
using LogTransformer.Application.Queries.GetAllLogsEntry;
using LogTransformer.Application.Commands.TransformLog;
using System.Collections.Generic;
using LogTransformer.Application.Queries.GetAllTransformedLogs;
using LogTransformer.Application.Queries.GetLogEntryById;
using LogTransformer.Application.Queries.GetTransformedLogById;

namespace LogTransformer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private readonly ILogEntryRepository _logEntryRepository;
        private readonly ITransformedLogRepository _transformedLogRepository;
        private readonly IMediator _mediator;

        public LogsController(ILogEntryRepository logEntryRepository, ITransformedLogRepository transformedLogRepository, IMediator mediator)
        {
            _logEntryRepository = logEntryRepository;
            _transformedLogRepository = transformedLogRepository;
            _mediator = mediator;
        }

        [HttpGet("saved")]
        public async Task<IActionResult> GetSavedLogs(int page = 0, int size = 3)
        {
            var result = await _mediator.Send(new GetAllLogsEntryQuery());

            return Ok(result);
        }

        [HttpGet("transformed")]
        public async Task<IActionResult> GetTransformedLogs()
        {
            var result = await _mediator.Send(new GetAllTransformedLogQuery());

            return Ok(result);
        }

        [HttpGet("saved/{id}")]
        public async Task<IActionResult> GetSavedLogById(int id)
        {
            var result = await _mediator.Send(new GetLogEntryByIdQuery(id));

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        [HttpGet("transformed/{id}")]
        public async Task<IActionResult> GetTransformedLogById(int id)
        {
            var result = await _mediator.Send(new GetTransformedLogByIdQuery(id));

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        [HttpGet("transformedByLog/{id}")]
        public async Task<IActionResult> GetTransformedLogByIdLog(int id)
        {
            var result = await _mediator.Send(new GetTransformedLogByLogIdQuery(id));

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
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
        public async Task<IActionResult> TransformLog([FromBody] TransformLogCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }
    }
}
