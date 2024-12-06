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
        public async Task<IActionResult> GetSavedLogs(string search = "", int page = 0, int size = 3)
        {
            var query = new GetAllLogsEntryQuery(search);

            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("transformed")]
        public async Task<IActionResult> GetTransformedLogs()
        {
            var logs = await _transformedLogRepository.GetAllTransformedLogsAsync();
            return Ok(logs);
        }

        [HttpGet("saved/{id}")]
        public async Task<IActionResult> GetSavedLogById(int id)
        {
            var log = await _logEntryRepository.GetLogByIdAsync(id);
            if (log == null)
            {
                return NotFound();
            }
            return Ok(log);
        }

        [HttpGet("transformed/{id}")]
        public async Task<IActionResult> GetTransformedLogById(int id)
        {
            var log = await _transformedLogRepository.GetTransformedLogByIdAsync(id);
            if (log == null)
            {
                return NotFound();
            }
            return Ok(log);
        }

        [HttpPost("save")]
        public async Task<IActionResult> SaveLog([FromBody] InsertLogEntryCommand command)
        {

            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Message);
            }

            return CreatedAtAction(nameof(GetSavedLogById), new { id = result.Data }, command);
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

        [HttpGet("sample-log")]
        public IActionResult GetSampleLog()
        {
            var logContents = new List<string>
            {
                "312|200|HIT|\"GET /robots.txt HTTP/1.1\"|100.2",
                "400|404|MISS|\"POST /index.html HTTP/1.1\"|150.5",
                "512|200|HIT|\"GET /home HTTP/1.1\"|90.3"
            };

            var concatenatedLogs = string.Join("\n", logContents);

            return Ok(logContents);
        }

    }
}
