using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherApp
{
    public class Hourly
    {
        public string Summary { get; set; }
        public string Icon { get; set; }
        public IEnumerable<WeatherData> Data { get; set; }
        public Hourly(Domain.ThirdParty.DarkSky.Hourly hourly)
        {
            Summary = hourly.summary;
            Icon = hourly.summary;
            Data = hourly.data.Select(d => new WeatherData(d));
        }
    }
}
