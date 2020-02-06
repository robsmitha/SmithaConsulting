using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherApp
{
    public class Daily
    {
        public string Summary { get; set; }
        public string Icon { get; set; }
        public IEnumerable<WeatherData> Data { get; set; }
        public Daily(Domain.ThirdParty.DarkSky.Daily daily)
        {
            Summary = daily.summary;
            Icon = daily.summary;
            Data = daily.data.Select(d => new WeatherData(d));
        }
    }
}
