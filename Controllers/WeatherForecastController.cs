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

        [HttpGet(Name = "GetWeatherForecast")]
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

        [HttpPost("/postv1")]
        public string Post([FromBody] WeatherForecast wf)
        {
            _logger.LogInformation(wf.ToString());
            return "posted";
        }

        [HttpPost("/postv2")]
        public string Postv2(WeatherForecast wf)
        {
            _logger.LogInformation(wf.ToString());
            return "posted";
        }

        [HttpGet("/product")]
        public List<Product> GetProduct()
        {
            _logger.LogInformation("get request for product from client #" + ++count);
            return appDBContext.Product.ToList();
        }
    }
}
