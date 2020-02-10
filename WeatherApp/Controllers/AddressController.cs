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
            var addressResult = _api.Get<Domain.ThirdParty.GoogleGeocode.Response>(!string.IsNullOrEmpty(address) ? $"Geocode/{address}" : $"Geocode/ReverseGeocode/{lat},{lng}");
            return new Address(address, addressResult.results.FirstOrDefault());
        }
    }
}