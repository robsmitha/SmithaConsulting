using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ROD.Data;
using ROD.Models;

namespace ROD.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var order = new Order
            {
                ID = 1,
            };
            var orderView = new OrderViewModel
            {
                Order = order
            };
            var items = new List<Item>
            {
                new Item { ID = 1, ItemName = "Item 1", Price = 29.99M },
                new Item { ID = 2, ItemName = "Item 2", Price = 14.99M },
                new Item { ID = 3, ItemName = "Item 3", Price = 4.99M },
                new Item { ID = 4, ItemName = "Item 4", Price = 59.99M }
            };
            var model = new RegisterViewModel(items);
            model.CurrentOrder = orderView;
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddLineItem(RegisterViewModel model)
        {
            var success = false;
            //TODO: options
            using (var context = new RODContext())
            {
                var order = new Order
                {

                };

                var lineItems = new List<LineItem>
                {
                    new LineItem
                    {
                        ItemAmount = 29.99M,
                        OrderID = order.ID,
                        Order = order,
                    },
                };

                try
                {
                    success = context.SaveChanges() > 0;
                }
                catch (Exception e)
                {
                    success = false;
                }
            }
            return Json(new { success });
        }
        public IActionResult About()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var nameSpace = "ROD.Models";
            var entities = assembly.GetTypes()
                      .Where(t => String.Equals(t.Namespace, nameSpace, StringComparison.Ordinal))
                      .ToArray();

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
