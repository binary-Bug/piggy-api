using AngularWebApi.Data;
using AngularWebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace AngularWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly AppDBContext appDBContext;

        public WeatherForecastController(AppDBContext appDBContext)
        {
            this.appDBContext = appDBContext;
        }

        [HttpGet("get/v1",Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("get/v2")]
        public IEnumerable<WeatherForecast> Getv2()
        {
            return appDBContext.WeatherForecasts.ToList();
        }

        [HttpPost("post")]
        public string Post(WeatherForecast wf)
        {
            appDBContext.WeatherForecasts.Add(wf);
            appDBContext.SaveChanges();
            return "posted";
        }
    }
}
