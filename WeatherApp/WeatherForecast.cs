using System;

namespace WeatherApp
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        //public int TemperatureC { get; set; }

        //public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
        public int TemperatureF { get; set; }
        public int TemperatureC => (int)((TemperatureF - 32) * 0.5556); //32 - (int)(TemperatureF * 0.5556);
        public string Summary { get; set; }
    }
}
