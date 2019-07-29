using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using rod;
using rodcon.Models;

namespace rodcon.Controllers
{
    public class RegisterController : Controller
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

            return Json(new { success });
        }
    }
}