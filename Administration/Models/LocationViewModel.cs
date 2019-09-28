using Newtonsoft.Json;
using Administration.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Administration.Models
{
    public class LocationViewModel
    {
        public LocationViewModel(string jsonPayload)
        {
            dynamic location = JsonConvert.DeserializeObject(jsonPayload);
            if (location?.status == "OK")
            {
                var l = location.results[0];
                var components = l.address_components;
                FormattedAddress = l.formatted_address;
                foreach (var c in components)
                {
                    foreach (var t in c.types)
                    {
                        switch (t.ToString())
                        {
                            case "street_number":
                                Street1Number = c.long_name;
                                break;
                            case "political":
                                break;
                            case "locality":
                                City = c.long_name;
                                break;
                            case "administrative_area_level_2":
                                County = c.long_name;
                                break;
                            case "administrative_area_level_1":
                                State = c.long_name;
                                StateAbbreviation = c.short_name;
                                break;
                            case "country":
                                Country = c.long_name;
                                CountryAbbreviation = c.short_name;
                                break;
                            case "postal_code":
                                Zip = c.long_name;
                                break;
                            case "":
                                Street1 = c.long_name;
                                break;
                        }
                    }
                }
                var latLng = l.geometry.location;
                Latitude = latLng.lat;
                Longitude = latLng.lng;
            }

        }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string FormattedAddress { get; set; }
        public string Street1Number { get; set; }
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string County { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string StateAbbreviation { get; set; }
        public string Country { get; set; }
        public string CountryAbbreviation { get; set; }
        public string Zip { get; set; }

    }
}
