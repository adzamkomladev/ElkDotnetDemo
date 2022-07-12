using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DockerDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        private readonly ILogger<DemoController> _logger;

        public DemoController(ILogger<DemoController> logger)
        {
            _logger = logger;
        }

        [HttpGet("log-stuff")]
        public async Task<OkResult> LogStuff([FromQuery] string query)
        {
            _logger.LogInformation($"LOG-STUFF ENDPOINT QUERY PARAMS: {query}");
            return Ok();
        }
    }
}
