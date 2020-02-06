using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Utilities;

namespace WeatherApp
{
    public class WeatherData
    {
        public string Date { get; set; }
        public string Time { get; set; }
        public string Summary { get; set; }
        public string Icon { get; set; }
        public double Temperature { get; set; }
        public double FeelsLikeTemperature { get; set; }
        public double TemperatureHigh { get; set; }
        public double TemperatureLow { get; set; }
        public WeatherData(Domain.ThirdParty.DarkSky.Data data)
        {
            var dt = data.time.ToDateTime();
            Time = dt.ToShortTimeString();
            Date = dt.ToShortDateString();
            Summary = data.summary;
            Icon = data.summary;
            TemperatureHigh = data.temperatureHigh;
            TemperatureLow = data.temperatureLow;
            Temperature = data.temperature;
            FeelsLikeTemperature = data.apparentTemperature;
        }
    }
}
