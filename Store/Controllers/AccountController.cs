using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Models;
using Domain.Services;
using Domain.Utilities;
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _api.GetAsync<UserModel>($"/users/{UserID}");
                if (user != null && SecurePasswordHasher.Verify(model.OldPassword, user.Password))
                {
                    if (model.NewPassword == model.OldPassword)
                    {
                        //New password must be different that current password
                        ModelState.AddModelError("CustomError", $"New password must be different that current password.");
                    }
                    else if (model.NewPassword == model.ConfirmPassword)
                    {
                        user.Password = SecurePasswordHasher.Hash(model.NewPassword);
                        await _api.PutAsync($"/users/{UserID}", user);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        //passwords did not match
                        ModelState.AddModelError("CustomError", $"Passwords did not match.");
                    }
                }
                else
                {
                    //old password was not correct
                    ModelState.AddModelError("CustomError", $"Password was not correct.");
                }
            }
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
        public IActionResult SignOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}