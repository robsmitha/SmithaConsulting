﻿using System;
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

namespace Store.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IApiService api, IMapper mapper, ICacheService cache) : base(api, mapper, cache) { }

        public IActionResult Index()
        {
            var order = GetOrder();
            var model = new RegisterViewModel(order);
            return View(model);
        }
        public IActionResult List(int? orderId)
        {
            var items = _api.Get<IEnumerable<ItemModel>>($"/merchants/{MerchantID}/items").Where(x => x.ItemTypeID != (int)ItemTypeEnums.Discount).ToList();
            var model = new RegisterListViewModel(items);
            return PartialView(model);
        }
        public IActionResult LoadCart(int? orderId)
        {
            var order = GetOrder(orderId);
            var orderViewModel = GetOrderViewModel(order);
            var model = new RegisterCartViewModel(orderViewModel);
            return PartialView("Cart", model);
        }
        protected bool AddLineItem(ItemModel item, OrderModel order)
        {
            if (item?.ID > 0)
            {
                var lineItem = new LineItemModel
                {
                    ItemAmount = item.Price ?? 0M,
                    ItemID = item.ID,
                };
                lineItem.OrderID = order.ID;
                _api.Post("/lineitems", lineItem);
                return true;
            }
            return false;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddLineItem(RegisterViewModel model)
        {
            var msg = string.Empty;
            var orderTask = GetOrderAsync();
            var itemTask = _api.GetAsync<ItemModel>($"/items/{model.SelectedItemID}");
            await Task.WhenAll(orderTask, itemTask);
            var order = orderTask.Result;
            var item = itemTask.Result;
            bool success;
            try
            {
                //Create Order if needed
                if (order == null)
                {
                    order = new OrderModel
                    {
                        OrderStatusTypeID = (int)OrderStatusTypeEnums.Open,
                        MerchantID = MerchantID.Value,
                        CustomerID = CustomerID,
                        CreatedAt = DateTime.Now
                    };
                    order = _api.Post("/orders", order);
                }
                success = AddLineItem(item, order);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                success = false;
            }
            return Json(new { success, msg, orderId = order?.ID });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveLineItem(RegisterViewModel model)
        {
            var msg = string.Empty;
            var success = false;
            var order = GetOrder(model.CurrentOrderID);
            try
            {
                var lineItem = _api.Get<IEnumerable<LineItemModel>>("/lineitems")
                    .LastOrDefault(x => x.OrderID == order.ID && x.ItemID == model.SelectedItemID);
                if (lineItem != null)
                {
                    _api.Delete($"/lineitems/{lineItem.ID}");
                    UpdateDiscounts(order);
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                success = false;
            }
            return Json(new { success, msg, orderId = order?.ID });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveItem(RegisterViewModel model)
        {
            var msg = string.Empty;
            var success = false;
            var order = GetOrder(model.CurrentOrderID);
            try
            {
                _api.Delete($"/orders/{order.ID}/lineitems/{model.SelectedItemID}");
                UpdateDiscounts(order);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                success = false;
            }
            return Json(new { success, msg, orderId = order?.ID });
        }
        public async Task<IActionResult> Payment()
        {
            var order = await GetOrderAsync();
            if (order == null)
            {
                return NotFound();
            }

            try
            {
                var orderViewModel = GetOrderViewModel(order);
                var model = new PaymentViewModel(orderViewModel, order.ID);
                return View(model);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Payment(PaymentViewModel model)
        {
            try
            {
                var order = await GetOrderAsync(model.CurrentOrderID);
                if (order != null)
                {
                    if (ModelState.IsValid)
                    {
                        var customer = await _api.GetAsync<CustomerModel>($"/customers/{CustomerID.Value}");
                        customer.Email = model.Email;
                        customer.FirstName = model.FirstName;
                        customer.LastName = model.LastName;
                        _api.Put("/customer", customer);

                        var amount = _api.Get<IEnumerable<LineItemModel>>("/lineitems")
                            .Where(x => x.OrderID == order.ID).Sum(x => x.ItemAmount);
                        //Create payment on order.
                        var payment = new PaymentModel
                        {
                            OrderID = model.CurrentOrderID,
                            PaymentTypeID = (int)PaymentTypeEnums.CreditCardManual,
                            Amount = amount,
                            PaymentStatusTypeID = (int)PaymentStatusTypeEnums.Paid
                        };

                        _api.Post("/payments", payment);

                        order.OrderStatusTypeID = (int)OrderStatusTypeEnums.Paid;
                        _api.Put($"/orders/{order.ID}", order);

                        return RedirectToAction("Details", "Home", new { id = model.CurrentOrderID });
                    }
                }
                else
                {
                    ModelState.AddModelError("CustomError", $"Order not found.");
                }
                
                var orderViewModel = GetOrderViewModel(order);
                model = new PaymentViewModel(orderViewModel, order.ID);
                return View(model);
            }
            catch(Exception ex)
            {
                return RedirectToAction("Error");
            }

        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await GetOrderAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            var orderViewModel = GetOrderViewModel(order);
            return View(orderViewModel);
        }
        protected bool AddDiscount(OrderModel order, string lookupCode)
        {
            try
            {
                var discount = _api.Get<IEnumerable<ItemModel>>($"/merchants/{MerchantID}/items")
                    .FirstOrDefault(x => x.ItemTypeID == (int)ItemTypeEnums.Discount && x.LookupCode == lookupCode);
                if (discount != null)
                {
                    var amount = 0M;
                    switch (discount.PriceTypeID)
                    {
                        case (int)PriceTypeEnums.Fixed:
                            amount = discount.Price ?? 0;
                            break;
                        case (int)PriceTypeEnums.Variable:
                            var lineItems = _api.Get<IEnumerable<LineItemModel>>("/lineitems").Where(x => x.OrderID == order.ID);
                            amount = lineItems.Sum(x => x.ItemAmount) * discount.Percentage.Value;
                            break;
                    }
                    var lineItem = new LineItemModel
                    {
                        ItemAmount = amount > 0 ? amount * -1 : amount,
                        ItemID = discount.ID,
                        OrderID = order.ID
                    };
                    _api.Post("/lineitems", lineItem);
                    return true;
                }
            }
            catch
            {

            }
            return false;

        }
        protected bool UpdateDiscounts(OrderModel order)
        {
            try
            {
                var discounts = _api.Get<IEnumerable<ItemModel>>("/items")
                .Where(x => x.ItemTypeID == (int)ItemTypeEnums.Discount && x.MerchantID == MerchantID);
                foreach (var discount in discounts)
                {
                    var lineItemDiscount = _api.Get<ItemModel>($"/items/{discount.ID}");
                    if (lineItemDiscount?.ID > 0)
                    {

                        var amount = 0M;
                        switch (discount.PriceTypeID)
                        {
                            case (int)PriceTypeEnums.Fixed: //fixed
                                amount = discount.Price ?? 0;
                                break;
                            case (int)PriceTypeEnums.Variable: //variable
                                var lineItems = _api.Get<IEnumerable<LineItemModel>>("/lineitems").Where(x => x.OrderID == order.ID && x.ID != lineItemDiscount.ID);
                                amount = lineItems.Sum(x => x.ItemAmount) * discount.Percentage ?? 0;
                                break;
                        }

                        _api.Delete($"/lineitems/{lineItemDiscount.ID}");

                        if (amount != 0)
                        {
                            var lineItem = new LineItemModel
                            {
                                ItemAmount = amount > 0 ? amount * -1 : amount,
                                ItemID = discount.ID,
                                OrderID = order.ID
                            };
                            _api.Post("/lineitems", lineItem);
                        }
                    }
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ApplyDiscount(PaymentViewModel model)
        {
            var order = GetOrder(model.CurrentOrderID);
            if (AddDiscount(order, model.PromoCode))
            {

            }
            return RedirectToAction("Payment", new { orderId = model.CurrentOrderID });
        }
        public IActionResult Edit(int itemId, int orderId)
        {
            var model = new RegisterEditViewModel();
            var lineItems = _api.Get<IEnumerable<LineItemModel>>("/lineitems").Where(x => x.OrderID == orderId && x.ItemID == itemId);
            var lineItem = lineItems.FirstOrDefault();
            if (lineItem != null)
            {
                model.OrderID = lineItem.OrderID;
                model.ItemID = lineItem.ItemID;
                model.Quantity = lineItems.Count();
            }
            return PartialView(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(RegisterEditViewModel model)
        {
            var success = true;
            var msg = string.Empty;
            try
            {
                var orderTask = GetOrderAsync();
                var itemTask = _api.GetAsync<ItemModel>($"/items/{model.ItemID}");
                Task.WhenAll(orderTask, itemTask);
                var item = itemTask.Result;
                var order = orderTask.Result;
                var itemCount = order.LineItems.Count;
                if (itemCount > 0)
                {
                    if (model.Quantity > itemCount)
                    {
                        // add
                        for (int i = 0; i < model.Quantity - itemCount; i++)
                        {
                            success &= AddLineItem(item, order);
                        }
                    }
                    else if (model.Quantity < itemCount)
                    {
                        // remove
                        for (int i = 0; i < itemCount - model.Quantity && i < itemCount; i++)
                        {
                            _api.Delete($"/lineItems/{order.LineItems[i].ID}");
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                msg = ex.Message;
                success = false;
            }
            return Json(new { success, msg, orderId = model.OrderID });
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
