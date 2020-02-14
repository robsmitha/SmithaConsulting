﻿using System;
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
            if (customer != null && SecurePasswordHasher.Verify(data.Password, customer.Password))
            {

            }
            return customer;
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
    }
}