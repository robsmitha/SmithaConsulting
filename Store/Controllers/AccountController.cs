using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Models;
using Domain.Services;
using Domain.Utilities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        public async Task<IActionResult> Dashboard()
        {
            var lineItems = await _api.GetAsync<IEnumerable<LineItemModel>>($"/merchants/{MerchantID}/lineItems");
            var yearEarnings = lineItems.Sum(x => x.ItemAmount);
            var monthEarnings = lineItems.Where(x => x.CreatedAt.Month == DateTime.Now.Month).Sum(x => x.ItemAmount);
            var yearOrderCount = lineItems.GroupBy(x => x.OrderID).Count();
            var monthOrderCount = lineItems.Where(x => x.CreatedAt.Month == DateTime.Now.Month).GroupBy(x => x.OrderID).Count();
            var model = new DashboardViewModel
            {
                YearEarnings = yearEarnings,
                MonthEarnings = monthEarnings,
                YearOrderCount = yearOrderCount,
                MonthOrderCount = monthOrderCount,
            };
            return View(model);
        }
        public async Task<IActionResult> Earnings(string interval)
        {

            var year = DateTime.Now.Year;
            var month = DateTime.Now.Month;

            IEnumerable<LineItemModel> lineItems = await _api.GetAsync<IEnumerable<LineItemModel>>($"/merchants/{MerchantID}/lineItems"); 
            var earnings = new List<decimal>();
            var labels = new List<string>();

            DateTime startDate = DateTime.MinValue;
            switch (interval)
            {
                case "hourly":
                    startDate = DateTime.Now.Date;
                    lineItems = lineItems
                    .Where(x => x.OrderCreatedAt.Date == startDate.Date && x.OrderOrderStatusTypeID == 2);
                    for (int i = 0; i < 24; i++)
                    {
                        var currentHour = startDate.AddHours(i);
                        var label = currentHour.ToShortTimeString();
                        var sales = lineItems.Where(x => x.OrderCreatedAt.Hour == currentHour.Hour).Sum(x => x.ItemAmount);
                        earnings.Add(sales);
                        labels.Add(label);
                    }
                    break;
                case "daily":
                    int diff = (7 + ((int)DateTime.Now.DayOfWeek - 1)) % 7;
                    startDate = DateTime.Now.AddDays(-1 * diff).Date;
                    lineItems = lineItems
                        .Where(x => x.OrderCreatedAt >= startDate && x.OrderCreatedAt <= startDate.AddDays(7)
                        && x.OrderOrderStatusTypeID == 2);

                    for (var i = 0; i < 7; i++)
                    {
                        var currentDay = startDate.AddDays(i);
                        var label = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedDayName(currentDay.DayOfWeek);
                        var sales = lineItems.Where(x => x.OrderCreatedAt.Date == currentDay.Date).Sum(x => x.ItemAmount);
                        earnings.Add(sales);
                        labels.Add(label);
                    }
                    break;
                case "monthly":
                    int daysInMonth = DateTime.DaysInMonth(year, month);
                    startDate = DateTime.Parse($"{month}/1/{year}");
                    var endDate = DateTime.Parse($"{month}/{daysInMonth}/{year}");
                    lineItems = lineItems
                    .Where(x => x.OrderCreatedAt.Month == startDate.Month && x.OrderOrderStatusTypeID == 2);
                    while (startDate <= endDate)
                    {
                        var label = startDate.ToShortDateString();
                        var sales = lineItems.Where(x => x.OrderCreatedAt.Date == startDate.Date).Sum(x => x.ItemAmount);
                        earnings.Add(sales);
                        labels.Add(label);
                        startDate = startDate.AddDays(1);
                    }
                    break;
                case "yearly":
                    lineItems = lineItems
                    .Where(x => x.OrderCreatedAt.Year == DateTime.Now.Year && x.OrderOrderStatusTypeID == 2);
                    for (var i = 1; i <= 12; i++)
                    {
                        var label = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(i);
                        var sales = lineItems.Where(x => x.OrderCreatedAt.Month == i).Sum(x => x.ItemAmount);
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
        public async Task<IActionResult> RevenueSources(string interval)
        {
            var year = DateTime.Now.Year;
            var month = DateTime.Now.Month;

            IEnumerable<LineItemModel> lineItems = await _api.GetAsync<IEnumerable<LineItemModel>>($"/merchants/{MerchantID}/lineItems");

            var earnings = new List<decimal>();
            var labels = new List<string>();
            var colors = new List<string>();

            DateTime startDate = DateTime.MinValue;

            switch (interval)
            {
                case "hourly":
                    startDate = DateTime.Now.Date;
                    lineItems = lineItems
                    .Where(x => x.OrderCreatedAt.Date == startDate.Date && x.OrderOrderStatusTypeID == 2);
                    break;
                case "daily":
                    int diff = (7 + ((int)DateTime.Now.DayOfWeek - 1)) % 7;
                    startDate = DateTime.Now.AddDays(-1 * diff).Date;
                    lineItems = lineItems
                    .Where(x => x.OrderCreatedAt >= startDate && x.OrderCreatedAt <= startDate.AddDays(7) && x.OrderOrderStatusTypeID == 2);
                    break;
                case "monthly":
                    int daysInMonth = DateTime.DaysInMonth(year, month);
                    startDate = DateTime.Parse($"{month}/1/{year}");
                    var endDate = DateTime.Parse($"{month}/{daysInMonth}/{year}");
                    lineItems = lineItems
                    .Where(x => x.OrderCreatedAt.Month == startDate.Month && x.OrderOrderStatusTypeID == 2);
                    break;
                case "yearly":
                    lineItems = lineItems
                    .Where(x => x.OrderCreatedAt.Year == DateTime.Now.Year && x.OrderOrderStatusTypeID == 2);
                    break;
            }
            var merchantLineItems = lineItems.GroupBy(x => x.OrderMerchantID).Select(f => f.FirstOrDefault());
            var rnd = new Random();
            foreach (var item in merchantLineItems)
            {
                Color randomColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
                var merchantName = item.OrderMerchantMerchantName;
                var sales = lineItems.Where(x => x.OrderMerchantID == item.OrderMerchantID).Sum(x => x.ItemAmount);
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