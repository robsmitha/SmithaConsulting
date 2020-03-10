using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CapitalData.Utilities;
using Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProPublicaSDK;

namespace CapitalData.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected readonly IApiService _api;
        protected readonly ProPublica _proPublica;
        protected static readonly string DefaultCongress = ConfigurationManager.GetConfiguration("DefaultCongress");
        protected static readonly string SenateChamber = ConfigurationManager.GetConfiguration("SenateChamber");
        protected static readonly string HouseChamber = ConfigurationManager.GetConfiguration("HouseChamber");
        public BaseController(IApiService api, ProPublica proPublica = null)
        {
            _api = api;
            _proPublica = proPublica;
        }
    }
}