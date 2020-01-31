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
    public class WeatherController : ControllerBase
    {
        private string Endpoint = ConfigurationManager.GetConfiguration("DarkSkyEndpoint");
        private string Token = ConfigurationManager.GetConfiguration("DarkSkySecretKey");

        [HttpGet("{lat},{lng}")]
        public ActionResult<Domain.ThirdParty.DarkSky.Response> Get(decimal lat, decimal lng)
        {
            var client = new ApiService(Endpoint, Token);

            var function = $"{Token}/{lat},{lng}";
            var response = client.Get<Domain.ThirdParty.DarkSky.Response>(function);

            return response;
        }
    }
}