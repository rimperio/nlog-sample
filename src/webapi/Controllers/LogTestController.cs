using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace nlog_sample.Controllers
{
    [ApiController]
    [Route("api")]
    public class LogTestController : ControllerBase
    {
        private static int requestCounter = 0;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<LogTestController> _logger;

        public LogTestController(ILogger<LogTestController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("weather")]
        public IActionResult Get()
        {
            try {
                _logger.LogInformation($"weather api called at: {DateTime.Now}");

                requestCounter++;

                if (requestCounter % 3 == 0 ){
                    var x = requestCounter / (requestCounter * 0);
                }

                var rng = new Random();
                var weather = new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(1),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                };

                return Ok(weather);
            } catch (Exception ex) {
                _logger.LogError(ex, $"Exception logged at: {DateTime.Now}");
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
