using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using MediatR;

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
            var query = new Features.Telegraf.Details.Query(body);
            var response = await _mediator.Send(query);

            if (response.Success)
            {
                return Ok();
            }
            else
            {
                return StatusCode(500);
            }
        }
    }
}
