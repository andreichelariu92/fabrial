using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace fabrial.Controllers
{
    [Route("[controller]/v1")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        [HttpPost("default")]
        public Task<IActionResult> CreateDefaultCommand()
        {
            var result = Created("default", null);
            return Task.FromResult((IActionResult)result);
        }

        [HttpPost("custom")]
        public Task<IActionResult> CreateCustomCommand()
        {
            var result = Created("custom", null);
            return Task.FromResult((IActionResult)result);
        }
    }
}
