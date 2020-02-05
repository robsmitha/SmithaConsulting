using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Utilities;
using Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeocodeController : ControllerBase
    {
        private string Endpoint = ConfigurationManager.GetConfiguration("GoogleGeocodeEndpoint");
        private string Key = ConfigurationManager.GetConfiguration("GoogleApiKey");

        [HttpGet("{address}")]
        public ActionResult<Domain.ThirdParty.GoogleGeocode.Response> Geocode(string address)
        {
            var client = new ApiService(Endpoint, Key);

            var function = $"address={address}&key={Key}";
            var response = client.Get<Domain.ThirdParty.GoogleGeocode.Response>(function);

            return response;
        }
        [HttpGet("ReverseGeocode/{lat},{lng}")]
        public ActionResult<Domain.ThirdParty.GoogleGeocode.Response> ReverseGeocode(double lat, double lng)
        {
            var client = new ApiService(Endpoint, Key);

            var function = $"latlng={lat},{lng}&key={Key}";
            var response = client.Get<Domain.ThirdParty.GoogleGeocode.Response>(function);

            return response;
        }
    }
}