using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using MediatR;
using System;

namespace SolarToTelegrafHTTPProxy.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TelegrafController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<TelegrafController> _logger;

        public TelegrafController(IMediator mediator, ILogger<TelegrafController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes("text/plain")]
        public async Task<IActionResult> PostData([FromBody] string body)
        {
            _logger.LogInformation($"Incoming request: {body}");

            try
            {
                var trimmedBody = body.Trim('\r', '\n');
                var splitData = trimmedBody.Split(',');

                // Return if the request is not detailed monitoring information
                if (splitData.GetValue(3) as string != "DT") return Ok();

                var query = new Features.Telegraf.Details.Query(splitData);
                var response = await _mediator.Send(query);

                return response.Success ? Ok() : StatusCode(500);
            } 
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error when parsing data");
                return StatusCode(500);
            }
        }
    }
}
