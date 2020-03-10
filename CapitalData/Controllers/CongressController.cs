using System.Collections.Generic;
using CapitalData.Utilities;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using ProPublicaSDK;
using ProPublicaSDK.Models;

namespace CapitalData.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CongressController : BaseController
    {
        public CongressController(IApiService api, ProPublica proPublica) : base(api, proPublica) { }
        
        [HttpGet("Members")]
        public List<MemberModel> GetMembers(string congress = null, string chamber = null)
        {
            congress ??= DefaultCongress;
            chamber ??= SenateChamber;
            var members = _proPublica.Members.GetMembers(congress, chamber);
            return members;
        }
    }
}