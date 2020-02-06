using System;

namespace WeatherApp
{
    public class WeatherForecast
    {
        public Currently Currently { get; set; }
        public Minutely Minutely { get; set; }
        public Hourly Hourly { get; set; }
        public Daily Daily { get; set; }
        public WeatherForecast(Domain.ThirdParty.DarkSky.Response response)
        {
            Currently = new Currently(response.currently);
            Minutely = new Minutely(response.minutely);
            Hourly = new Hourly(response.hourly);
            Daily = new Daily(response.daily);
        }
    }
}
