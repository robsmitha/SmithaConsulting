using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Enums;
using DomainLayer.Models;
using DomainLayer.Services;
using Microsoft.AspNetCore.Mvc;
using Store.Models;

namespace Store.Controllers
{
    public class RegisterController : BaseController
    {
        public RegisterController(IApiService api, IMapper mapper, ICacheService cache) : base(api, mapper, cache) { }
        public async Task<IActionResult> Index()
        {
            var order = await GetOrderAsync();
            var model = new RegisterViewModel(order, UserID);
            return View(model);
        }
        public async Task<IActionResult> List(int? orderId)
        {
            var items = await _api.GetAsync<IEnumerable<ItemModel>>($"/merchants/{MerchantID}/items");
            var model = new InventoryViewModel(items.Where(x => x.ItemTypeID != (int)ItemTypeEnums.Discount).ToList());
            return PartialView("_List", model);
        }
        public async Task<IActionResult> LoadCart(int? orderId)
        {
            var order = await GetOrderAsync(orderId);
            var orderViewModel = GetOrderViewModel(order);
            var model = new CartViewModel(orderViewModel);
            return PartialView("_Cart", model);
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
        public async Task<IActionResult> RemoveLineItem(RegisterViewModel model)
        {
            var msg = string.Empty;
            var success = false;
            var order = await GetOrderAsync(model.CurrentOrderID);
            try
            {
                var lineItem = order.LineItems.LastOrDefault(x => x.OrderID == order.ID && x.ItemID == model.SelectedItemID);
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
        public async Task<IActionResult> RemoveItem(RegisterViewModel model)
        {
            var msg = string.Empty;
            var success = false;
            var order = await GetOrderAsync(model.CurrentOrderID);
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

        public async Task<IActionResult> Edit(int itemId, int orderId)
        {
            var model = new CartEditViewModel();
            var order = await GetOrderAsync(orderId);
            var lineItems = order.LineItems.Where(x => x.ItemID == itemId);
            var lineItem = lineItems.LastOrDefault();
            if (lineItem != null)
            {
                model.OrderID = lineItem.OrderID;
                model.ItemID = lineItem.ItemID;
                model.Quantity = lineItems.Count();
            }
            return PartialView("_Edit", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CartEditViewModel model)
        {
            var success = true;
            var msg = string.Empty;
            try
            {
                var orderTask = GetOrderAsync();
                var itemTask = _api.GetAsync<ItemModel>($"/items/{model.ItemID}");
                await Task.WhenAll(orderTask, itemTask);
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

    }
}