using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Enums;
using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Store.Models;

namespace Store.Controllers
{
    public class StoreController : BaseController
    {
        public StoreController(IApiService api, IMapper mapper, ICacheService cache) : base(api, mapper, cache) { }
        public IActionResult Index(int? categoryId)
        {
            ViewData["categoryId"] = categoryId ?? 0;
            var model = new ShopViewModel();
            return View(model);
        }
        public async Task<IActionResult> Inventory()
        {
            var items = await _api.GetAsync<IEnumerable<ItemModel>>($"/merchants/{MerchantID}/items");
            var model = new InventoryViewModel(items.Where(x => x.ItemTypeID != (int)ItemTypeEnums.Discount).ToList());
            return PartialView("_Inventory", model);
        }
        public async Task<IActionResult> Item(int id)
        {
            var item = await _api.GetAsync<ItemModel>($"/items/{id}");
            var model = _mapper.Map<ItemViewModel>(item);
            return View(model);
        }
        public async Task<IActionResult> ItemCategories(int? categoryId)
        {
            ViewData["categoryId"] = categoryId ?? 0;
            var categories = await _api.GetAsync<IEnumerable<ItemCategoryTypeModel>>("/ItemCategoryTypes");
            var model = new ItemCategoryTypesViewModel(categories?.ToList());
            return PartialView("_ItemCategories", model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddLineItem(ItemViewModel model)
        {
            var msg = string.Empty;
            var orderTask = GetOrderAsync();
            var itemTask = _api.GetAsync<ItemModel>($"/items/{model.ID}");
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
    }
}