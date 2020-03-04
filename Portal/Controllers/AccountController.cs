using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Services;
using Domain.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Portal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IApiService _api;

        public AccountController(ILogger<AccountController> logger, IApiService api)
        {
            _logger = logger;
            _api = api;
        }

        [HttpPost("SignIn")]
        public async Task<CustomerModel> SignIn(CustomerModel data)
        {
            var customer = await _api.PostAsync($"/customers/SignIn/", data);
            if (SecurePasswordHasher.Verify(data.Password, customer.Password))
            {
                return customer;
            }
            return null;
        }
        [HttpPost("SignUp")]
        public async Task<CustomerModel> SignUp(CustomerModel data)
        {
            data.Password = SecurePasswordHasher.Hash(data.Password);
            return await _api.PostAsync($"/customers/", data);
        }
        [HttpGet("Profile")]
        public async Task<CustomerModel> Profile(int id)
        {
            return await _api.GetAsync<CustomerModel>($"/customers/{id}");
        }

        [HttpPost("EditProfile")]
        public async Task<CustomerModel> EditProfile(CustomerModel data)
        {
            if(!SecurePasswordHasher.IsHashSupported(data.Password = SecurePasswordHasher.Hash(data.Password))){
                data.Password = SecurePasswordHasher.Hash(data.Password);
            }
            return await _api.PutAsync($"/customers/{data.ID}", data);
        }
    }
}