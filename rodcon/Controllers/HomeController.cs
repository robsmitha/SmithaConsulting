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
using rod.Enums;
using System.Net.Mail;
using System.Net;

namespace rodcon.Controllers
{
    public class HomeController : BaseController
    {
        private readonly rodContext _context;

        public HomeController(rodContext context) : base(context)
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
        public IActionResult Contact(ContactViewModel model)
        {
            var fromAddress = new MailAddress("wmcmailer@gmail.com", "Website Mailer");
            var toAddress = new MailAddress("robsmitha94@gmail.com", "Rob Smitha");
            const string fromPassword = "RvY0rIWyP2H6T29pZDtpOSTQ8l0218BX";
            const string subject = "Website Mail";
            string body = $"From: {model.Email}, {model.Name} message: {model.Message}";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }

            return RedirectToAction("Index");
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
                    CreateUserSession(user);
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

                    //Put User in Online Merchant
                    var merchant = await _context.Merchants.FirstOrDefaultAsync(x => x.MerchantTypeID == (int)MerchantTypeEnums.Online && x.Active);
                    if (merchant != null)
                    {
                        var userMerchant = new MerchantUser
                        {
                            Active = true,
                            CreatedAt = DateTime.Now,
                            MerchantID = merchant.ID,
                            RoleID = (int)RoleEnums.OnlineSignUp,
                            UserID = user.ID
                        };
                        await _context.MerchantUsers.AddAsync(userMerchant);
                        await _context.SaveChangesAsync();
                    }
                    CreateUserSession(user);
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
