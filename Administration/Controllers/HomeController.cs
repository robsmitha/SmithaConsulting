using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Architecture;
using Administration.Models;
using Architecture.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Architecture.Utilities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Administration.Constants;
using Architecture.Enums;
using System.Net.Mail;
using System.Net;
using Administration.Utilities;

namespace Administration.Controllers
{
    public class HomeController : BaseController
    {
        private readonly DbArchitecture _context;

        public HomeController(DbArchitecture context) : base(context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var model = new HomeViewModel
            {
                AccessBlog = CheckPermission(permissionName: "AccessBlog"),
                AccessMerchants = CheckPermission(permissionName: "AccessMerchants"),
                AccessOrders = CheckPermission(permissionName: "AccessOrders"),
                AccessItems = CheckPermission(permissionName: "AccessItems"),
                AccessCustomers = CheckPermission(permissionName: "AccessCustomers"),
                AccessRoles = CheckPermission(permissionName: "AccessRoles"),
                AccessPermissions = CheckPermission(permissionName: "AccessPermissions"),
                AccessRolePermissions = CheckPermission(permissionName: "AccessRolePermissions"),
                AccessThemes = CheckPermission(permissionName: "AccessThemes")
            };
                //new HomeViewModel(UserPermissions.Where(x => !string.IsNullOrEmpty(x.Controller) && !string.IsNullOrEmpty(x.Action) && x.Action == "Index"));
            return View(model);
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
            var fromPassword = ConfigurationManager.GetString("GmailPassword");
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
                var user = await _context.Users
                    .SingleOrDefaultAsync(m => m.ID == UserID);
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
                        await _context.SaveChangesAsync();
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
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
                    //password was not correct
                    ModelState.AddModelError("CustomError", "Username or password was not correct.");
                }
            }
            return View(model);
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
        public async Task<IActionResult> SignUp(SignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (await _context.Users
                        .SingleOrDefaultAsync(m => m.Username == model.Username) == null)
                    {
                        if(model.Password == model.ConfirmPassword)
                        {
                            var user = new User
                            {
                                Email = model.Email,
                                FirstName = model.FirstName,
                                MiddleName = model.MiddleName,
                                LastName = model.LastName,
                                Username = model.Username,
                                Password = SecurePasswordHasher.Hash(model.Password),
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
                catch(Exception ex)
                {
                    return RedirectToAction("Error");
                }
            }
            return View(model);
        }
        public IActionResult About()
        {
            var @namespace = "Architecture";
            

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
