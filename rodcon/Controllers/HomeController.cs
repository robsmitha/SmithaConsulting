using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using rod;
using rodcon.Models;
using rod.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using rod.Utilities;
using Microsoft.AspNetCore.Mvc.Rendering;
using rodcon.Constants;

namespace rodcon.Controllers
{
    public class HomeController : BaseController
    {
        private readonly rodContext _context;

        public HomeController(rodContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginAsync(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users
                    .SingleOrDefaultAsync(m => m.Username == model.Username);
                if(user != null && SecurePasswordHasher.Verify(model.Password, user.Password))
                {
                    CreateUserSession(_context, user);
                    return RedirectToAction("Index");
                }
                else
                {

                }
            }
            return View("Login", model);
        }
        public IActionResult SignUp()
        {
            ViewData["MerchantID"] = new SelectList(_context.Merchants, "ID", "MerchantName");
            return View();
        }

        public IActionResult SignOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUpAsync(SignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (await _context.Users
                        .SingleOrDefaultAsync(m => m.Username == model.Username) != null)
                    {
                        return Json(new
                        {
                            success = false,
                            message = $"The Username {model.Username} is already taken."
                        });
                    }

                    var hashword = SecurePasswordHasher.Hash(model.Password);
                    var user = new User
                    {
                        Email = model.Email,
                        FirstName = model.FirstName,
                        MiddleName = model.MiddleName,
                        LastName = model.LastName,
                        Username = model.Username,
                        Password = hashword.ToString(),
                        CreatedAt = DateTime.Now,
                        Active = true
                    };
                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();
                    CreateUserSession(_context, user);
                    return RedirectToAction("Index");
                }
                catch(Exception ex)
                {
                    return RedirectToAction("Error");
                }
            }
            return View("SignUp", model);
        }
        public IActionResult About()
        {
            var @namespace = "rod";
            

            var entities = AppDomain.CurrentDomain.GetAssemblies()
                       .SelectMany(t => t.GetTypes())
                       .Where(t => t.IsClass && t.Namespace == @namespace).ToArray();

            ViewBag.entities = entities;

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
