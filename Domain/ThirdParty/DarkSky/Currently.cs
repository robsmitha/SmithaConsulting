using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ThirdParty.DarkSky
{
    public class Currently
    {
        public decimal time { get; set; }
        public string summary { get; set; }
        public string icon { get; set; }
        public decimal nearestStormDistance { get; set; }
        public decimal precipdecimalensity { get; set; }
        public decimal precipdecimalensityError { get; set; }
        public decimal precipProbability { get; set; }
        public string precipType { get; set; }
        public decimal temperature { get; set; }
        public decimal apparentTemperature { get; set; }
        public decimal dewPodecimal { get; set; }
        public decimal humidity { get; set; }
        public decimal pressure { get; set; }
        public decimal windSpeed { get; set; }
        public decimal windGust { get; set; }
        public decimal windBearing { get; set; }
        public decimal cloudCover { get; set; }
        public decimal uvIndex { get; set; }
        public decimal visibility { get; set; }
        public decimal ozone { get; set; }
    }
}
