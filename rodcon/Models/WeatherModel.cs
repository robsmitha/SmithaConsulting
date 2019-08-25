using Newtonsoft.Json;
using rodcon.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace rodcon.Models
{
    public class WeatherModel
    {
        public WeatherModel(string jsonPayload)
        {
            dynamic forecast = JsonConvert.DeserializeObject(jsonPayload);
            if (forecast != null)
            {
                Latitude = forecast.latitude;
                Longitude = forecast.longitude;
                Timezone = forecast.timezone;
                // Unix timestamp is seconds past epoch
                DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                Time = dtDateTime.AddSeconds(Convert.ToDouble(forecast.currently.time)).ToLocalTime();
                Summary = forecast.currently.summary;
                Icon = forecast.currently.icon;
                Temperature = Convert.ToDecimal(forecast.currently.temperature);
            }
        }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public decimal Temperature { get; set; }
        public string Timezone { get; set; }
        public DateTime Time { get; set; }
        public string Summary { get; set; }
        public string Icon { get; set; }
    }
}
