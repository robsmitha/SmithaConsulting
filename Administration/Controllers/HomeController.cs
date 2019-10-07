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
using System.Globalization;
using Newtonsoft.Json;
using System.Drawing;

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
            var lineItems = _context.LineItems
                .Include(o => o.Order)
                .Where(x => x.Order.CreatedAt.Year == DateTime.Now.Year && x.Order.OrderStatusTypeID == (int)OrderStatusTypeEnums.Paid);
            var yearEarnings = lineItems.Sum(x => x.ItemAmount);
            var monthEarnings = lineItems.Where(x => x.CreatedAt.Month == DateTime.Now.Month).Sum(x => x.ItemAmount);
            var yearOrderCount = lineItems.GroupBy(x => x.OrderID).Count();
            var monthOrderCount = lineItems.Where(x => x.CreatedAt.Month == DateTime.Now.Month).GroupBy(x => x.OrderID).Count();

            var model = new HomeViewModel
            {
                YearEarnings = yearEarnings,
                MonthEarnings = monthEarnings,
                YearOrderCount = yearOrderCount,
                MonthOrderCount = monthOrderCount,
            };
            return View(model);
        }

        public IActionResult Earnings(string interval)
        {
            var year = DateTime.Now.Year;
            var month = DateTime.Now.Month;

            IQueryable<LineItem> lineItems = null;

            var earnings = new List<decimal>();
            var labels = new List<string>();

            DateTime startDate = DateTime.MinValue;

            switch (interval)
            {
                case "hourly":
                    startDate = DateTime.Now.Date;
                    lineItems = _context.LineItems
                    .Include(o => o.Order)
                    .Where(x => x.Order.CreatedAt.Date == startDate.Date && x.Order.OrderStatusTypeID == (int)OrderStatusTypeEnums.Paid);
                    for (int i = 0; i < 24; i++)
                    {
                        var currentHour = startDate.AddHours(i);
                        var label = currentHour.ToShortTimeString();
                        var sales = lineItems.Where(x => x.Order.CreatedAt.Hour == currentHour.Hour).Sum(x => x.ItemAmount);
                        earnings.Add(sales);
                        labels.Add(label);
                    }
                    break;
                case "daily":
                    int diff = (7 + ((int)DateTime.Now.DayOfWeek - 1)) % 7;
                    startDate = DateTime.Now.AddDays(-1 * diff).Date;
                    lineItems = _context.LineItems
                        .Include(o => o.Order)
                        .Where(x => x.Order.CreatedAt >= startDate && x.Order.CreatedAt <= startDate.AddDays(7) 
                        && x.Order.OrderStatusTypeID == (int)OrderStatusTypeEnums.Paid);

                    for (var i = 0; i < 7; i++)
                    {
                        var currentDay = startDate.AddDays(i);
                        var label = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedDayName(currentDay.DayOfWeek);
                        var sales = lineItems.Where(x => x.Order.CreatedAt.Date == currentDay.Date).Sum(x => x.ItemAmount);
                        earnings.Add(sales);
                        labels.Add(label);
                    }
                    break;
                case "monthly":
                    int daysInMonth = DateTime.DaysInMonth(year, month);
                    startDate = DateTime.Parse($"{month}/1/{year}");
                    var endDate = DateTime.Parse($"{month}/{daysInMonth}/{year}");
                    lineItems = _context.LineItems
                    .Include(o => o.Order)
                    .Where(x => x.Order.CreatedAt.Month == startDate.Month && x.Order.OrderStatusTypeID == (int)OrderStatusTypeEnums.Paid);
                    while (startDate <= endDate)
                    {
                        var label = startDate.ToShortDateString();
                        var sales = lineItems.Where(x => x.Order.CreatedAt.Date == startDate.Date).Sum(x => x.ItemAmount);
                        earnings.Add(sales);
                        labels.Add(label);
                        startDate = startDate.AddDays(1);
                    }
                    break;
                case "yearly":
                    lineItems = _context.LineItems
                    .Include(o => o.Order)
                    .Where(x => x.Order.CreatedAt.Year == DateTime.Now.Year && x.Order.OrderStatusTypeID == (int)OrderStatusTypeEnums.Paid);
                    for (var i = 1; i <= 12; i++)
                    {
                        var label = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(i);
                        var sales = lineItems.Where(x => x.Order.CreatedAt.Month == i).Sum(x => x.ItemAmount);
                        earnings.Add(sales);
                        labels.Add(label);
                    }
                    break;
            }


            var model = new EarningsViewModel
            {
                EarningsJSON = JsonConvert.SerializeObject(earnings),
                LabelsJSON = JsonConvert.SerializeObject(labels),
            };

            return PartialView("_Earnings", model);
        }
        public IActionResult RevenueSources(string interval)
        {
            var year = DateTime.Now.Year;
            var month = DateTime.Now.Month;

            IQueryable<LineItem> lineItems = null;
            IQueryable<LineItem> merchantLineItems = null;

            var earnings = new List<decimal>();
            var labels = new List<string>();
            var colors = new List<string>();

            DateTime startDate = DateTime.MinValue;

            switch (interval)
            {
                case "hourly":
                    startDate = DateTime.Now.Date;
                    lineItems = _context.LineItems
                    .Include(o => o.Order)
                    .ThenInclude(o => o.Merchant)
                    .Where(x => x.Order.CreatedAt.Date == startDate.Date && x.Order.OrderStatusTypeID == (int)OrderStatusTypeEnums.Paid);
                    break;
                case "daily":
                    int diff = (7 + ((int)DateTime.Now.DayOfWeek - 1)) % 7;
                    startDate = DateTime.Now.AddDays(-1 * diff).Date;
                    lineItems = _context.LineItems
                    .Include(o => o.Order)
                    .ThenInclude(o => o.Merchant)
                    .Where(x => x.Order.CreatedAt >= startDate && x.Order.CreatedAt <= startDate.AddDays(7) && x.Order.OrderStatusTypeID == (int)OrderStatusTypeEnums.Paid);
                    break;
                case "monthly":
                    int daysInMonth = DateTime.DaysInMonth(year, month);
                    startDate = DateTime.Parse($"{month}/1/{year}");
                    var endDate = DateTime.Parse($"{month}/{daysInMonth}/{year}");
                    lineItems = _context.LineItems
                    .Include(o => o.Order)
                    .ThenInclude(o => o.Merchant)
                    .Where(x => x.Order.CreatedAt.Month == startDate.Month && x.Order.OrderStatusTypeID == (int)OrderStatusTypeEnums.Paid);
                    break;
                case "yearly":
                    lineItems = _context.LineItems
                    .Include(o => o.Order)
                    .ThenInclude(o => o.Merchant)
                    .Where(x => x.Order.CreatedAt.Year == DateTime.Now.Year && x.Order.OrderStatusTypeID == (int)OrderStatusTypeEnums.Paid);
                    break;
            }
            merchantLineItems = lineItems.GroupBy(x => x.Order.MerchantID).Select(f => f.FirstOrDefault());
            var rnd = new Random();
            foreach (var item in merchantLineItems)
            {
                Color randomColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
                var merchantName = item.Order.Merchant.MerchantName;
                var sales = lineItems.Where(x => x.Order.MerchantID == item.Order.MerchantID).Sum(x => x.ItemAmount);
                labels.Add(merchantName);
                earnings.Add(sales);
                colors.Add("#" + randomColor.R.ToString("X2") + randomColor.G.ToString("X2") + randomColor.B.ToString("X2"));
            }

            var model = new RevenueSourcesViewModel
            {
                RevenueSourcesJSON = JsonConvert.SerializeObject(earnings),
                LabelsJSON = JsonConvert.SerializeObject(labels),
                ColorsJSON = JsonConvert.SerializeObject(colors),
                Colors = colors,
                Labels = labels
            };

            return PartialView("_RevenueSources", model);
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

            using (var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            })
            {
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }
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
                            await _context.AddAsync(user);
                            await _context.SaveChangesAsync();

                            var merchant = new Merchant
                            {
                                MerchantName = model.MerchantName,
                                WebsiteUrl = model.WebsiteUrl,
                                MerchantTypeID = (int)MerchantTypeEnums.Online,
                                Active = true,
                                CreatedAt = DateTime.Now,
                                IsBillable = false,
                                SelfBoardingApplication = true
                            };
                            await _context.AddAsync(merchant);
                            await _context.SaveChangesAsync();

                            var userMerchant = new MerchantUser
                            {
                                Active = true,
                                CreatedAt = DateTime.Now,
                                MerchantID = merchant.ID,
                                RoleID = (int)RoleEnums.OnlineSignUp,
                                UserID = user.ID
                            };
                            await _context.AddAsync(userMerchant);
                            await _context.SaveChangesAsync();

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
        public IActionResult SignOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
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
