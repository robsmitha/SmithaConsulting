using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WeatherApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddressController : ControllerBase
    {
        private readonly ILogger<AddressController> _logger;
        private readonly IApiService _api;

        public AddressController(ILogger<AddressController> logger, IApiService api)
        {
            _logger = logger;
            _api = api;
        }
        [HttpGet]
        public Address Get(string address)
        {
            var locationResult = _api.Get<Domain.ThirdParty.GoogleGeocode.Response>($"Geocode/{address}");
            var location = locationResult.results.FirstOrDefault();
            double? lat = location?.geometry.location.lat;
            double? lng = location?.geometry.location.lng;
            string formattedAddress = location?.formatted_address;
            return new Address(enteredAddress: address, formattedAddress: formattedAddress, lat: lat, lng: lng);
        }
    }
}