using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Services;
using Microsoft.AspNetCore.Mvc;
using Store.Models;

namespace Store.Controllers
{
    public class AccountController : BaseController
    {
        public AccountController(IApiService api, IMapper mapper, ICacheService cache) : base(api, mapper, cache) { }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Business(int id)
        {
            return View();
        }
        public IActionResult ChangePassword()
        {
            var model = new ChangePasswordViewModel();
            return View(model);
        }
        public async Task<IActionResult> Order(int id)
        {
            var order = await GetOrderAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            var orderViewModel = GetOrderViewModel(order);
            return View(orderViewModel);
        }
    }
}