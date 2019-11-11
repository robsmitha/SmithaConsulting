using DomainLayer.Constants;
using Newtonsoft.Json;

namespace DomainLayer.Services.GoogleGeocode
{
    public class Location
    {
        public Location(string jsonPayload)
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
                            case GoogleGeocodeConstants.STREET_NUMBER:
                                Street1Number = c.long_name;
                                break;
                            case GoogleGeocodeConstants.POLITICAL:
                                break;
                            case GoogleGeocodeConstants.LOCALITY:
                                City = c.long_name;
                                break;
                            case GoogleGeocodeConstants.ADMINISTRATIVE_AREA_LEVEL_2:
                                County = c.long_name;
                                break;
                            case GoogleGeocodeConstants.ADMINISTRATIVE_AREA_LEVEL_1:
                                State = c.long_name;
                                StateAbbreviation = c.short_name;
                                break;
                            case GoogleGeocodeConstants.COUNTRY:
                                Country = c.long_name;
                                CountryAbbreviation = c.short_name;
                                break;
                            case GoogleGeocodeConstants.POSTAL_CODE:
                                Zip = c.long_name;
                                break;
                            case GoogleGeocodeConstants.STREET_1:
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
