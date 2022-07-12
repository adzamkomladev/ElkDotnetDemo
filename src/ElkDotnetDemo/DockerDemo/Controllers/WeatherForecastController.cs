using Microsoft.AspNetCore.Mvc;

namespace DockerDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            var temps =  Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();

            var firstTemp = temps.First();

            _logger.LogInformation($"THIS IS THE FIRST TEMPERATURE: {firstTemp.TemperatureC} C");

            if (firstTemp.TemperatureC < 0)
            {
                _logger.LogWarning($"FIRST TEMPERATURE IS LESS THAN 0 DEGREES. {firstTemp.TemperatureC}");
            }

            return temps;
        }
    }
}