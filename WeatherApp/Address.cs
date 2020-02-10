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
        public Address(string address, Domain.ThirdParty.GoogleGeocode.Result location)
        {
            EnteredAddress =  address;
            FormattedAddress = location?.formatted_address;
            Latitude= location?.geometry.location.lat ?? 0;
            Longitude = location?.geometry.location.lng ?? 0;
        }
    }
}
