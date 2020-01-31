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
        public IEnumerable<WeatherForecast> Get()
        {
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            var result = _api.Get<Domain.ThirdParty.DarkSky.Response>("Weather/42.3601,-71.0589");
            var model = result.daily.data.Select(d => new WeatherForecast
            {
                Date = dtDateTime.AddSeconds(Convert.ToDouble(d.time)).ToLocalTime(),
                Summary = d.summary,
                TemperatureF = (int)d.temperatureHigh
            }).ToList();

            return model;
        }
    }
}
