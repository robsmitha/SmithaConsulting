using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherApp
{
    public class Address
    {
        public string EnteredAddress { get; set; }
        public string FormattedAddress { get; set; }
        public bool ValidAddress => Latitude != 0 && Longitude != 0;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public Address(string enteredAddress, string formattedAddress = null, double? lat = null, double? lng = null)
        {
            EnteredAddress = enteredAddress;
            FormattedAddress = formattedAddress;
            Latitude = lat ?? 0;
            Longitude = lng ?? 0;
        }
    }
}
