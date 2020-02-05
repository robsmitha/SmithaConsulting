using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WeatherApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IApiService _api;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IApiService api)
        {
            _logger = logger;
            _api = api;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get(double lat, double lng)
        {
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            var weatherResult = _api.Get<Domain.ThirdParty.DarkSky.Response>($"Weather/{lat},{lng}");
            var model = weatherResult.daily.data.Select(d => new WeatherForecast
            {
                Date = dtDateTime.AddSeconds(Convert.ToDouble(d.time)).ToLocalTime(),
                Summary = d.summary,
                TemperatureF = (int)d.temperatureHigh
            }).ToList();

            return model;
        }
    }
}
