using fabrial.ApplicationLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace fabrial.Controllers
{
    [Route("[controller]/v1")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly CreateSqlCommandUsecase createSqlCommandUsecase;

        public CommandsController(CreateSqlCommandUsecase createSqlCommandUsecase)
        {
            this.createSqlCommandUsecase = createSqlCommandUsecase;
        }

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

        [HttpPost("sql")]
        public Task<IActionResult> CreateSqlCommand([FromBody] string sql)
        {
            var result = createSqlCommandUsecase.Execute(sql);
            if (result == null || result.Rows != null)
            {
                return Task.FromResult((IActionResult)StatusCode(500, result?.ErrorDetail));
            }

            return Task.FromResult((IActionResult)Ok(result.Rows));
        }
    }
}
