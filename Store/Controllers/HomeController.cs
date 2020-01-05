using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DomainLayer.Enums;
using Store.Models;
using DomainLayer.Models;
using DomainLayer.Services;
using AutoMapper;
using DomainLayer.Utilities;
using DomainLayer.Entities;
using Microsoft.AspNetCore.Http;
using Store.Constants;

namespace Store.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IApiService api, IMapper mapper, ICacheService cache) : base(api, mapper, cache) { }

        public IActionResult Index()
        {
            var userId = HttpContext.Session?.GetInt32(SessionKeysConstants.USER_ID);
            var model = new HomeViewModel(userId);
            return View(model);
        }

        public IActionResult Documentation()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _api.GetAsync<UserModel>($"/users/GetByUsername/{model.Username}");
                if (user != null && SecurePasswordHasher.Verify(model.Password, user.Password))
                {
                    if (!user.Active)
                    {
                        //user has not been approved by an admin yet
                        ModelState.AddModelError("CustomError", "Your account is inactive until an admin activates it.");
                    }
                    CreateUserSession(user);
                    return RedirectToAction("Index");
                }
                else
                {
                    //password was not correct
                    ModelState.AddModelError("CustomError", "Username or password was not correct.");
                }
            }
            return View(model);
        }
        public IActionResult SignUp()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _api.GetAsync<UserModel>($"/users/GetByUsername/{model.Username}");
                    if (user == null)
                    {
                        if (model.Password == model.ConfirmPassword)
                        {
                            user = new UserModel 
                            {
                                Email = model.Email,
                                FirstName = model.FirstName,
                                MiddleName = model.MiddleName,
                                LastName = model.LastName,
                                Username = model.Username,
                                Password = SecurePasswordHasher.Hash(model.Password),
                                Active = true,
                                MerchantName = model.MerchantName,
                                WebsiteUrl = model.WebsiteUrl
                            };
                            user = await _api.PostAsync($"/users", user);
                            CreateUserSession(user);
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError("CustomError", $"Passwords did not match.");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("CustomError", $"The Username {model.Username} is already taken.");
                    }
                }
                catch (Exception)
                {
                    return RedirectToAction("Error");
                }
            }
            return View(model);
        }
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
