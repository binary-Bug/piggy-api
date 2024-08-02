using AngularWebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace AngularWebApi.Controllers
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
        private readonly AppDBContext appDBContext;
        public static int count = 0;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, AppDBContext appDBContext)
        {
            _logger = logger;
            this.appDBContext = appDBContext;
        }

        [HttpGet("[controller]/get/v1",Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            _logger.LogInformation("get request from client #" + ++count);
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("[controller]/get/v2")]
        public IEnumerable<WeatherForecast> Getv2()
        {
            _logger.LogInformation("get request from client #" + ++count);
            return appDBContext.WeatherForecasts.ToList();
        }

        [HttpPost("[controller]/post")]
        public string Post(WeatherForecast wf)
        {
            _logger.LogInformation(wf.ToString());
            appDBContext.WeatherForecasts.Add(wf);
            appDBContext.SaveChanges();
            return "posted";
        }
    }
}
