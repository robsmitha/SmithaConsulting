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
        public Address Get(string address = null, double? lat = null, double? lng = null)
        {
            var locationResult = !string.IsNullOrEmpty(address)
                ? _api.Get<Domain.ThirdParty.GoogleGeocode.Response>($"Geocode/{address}")
                : _api.Get<Domain.ThirdParty.GoogleGeocode.Response>($"Geocode/ReverseGeocode/{lat},{lng}");
            
            var location = locationResult.results.FirstOrDefault();
            
            return new Address(
                enteredAddress: address, 
                formattedAddress: location?.formatted_address, 
                lat: location?.geometry.location.lat, 
                lng: location?.geometry.location.lng);
        }
    }
}