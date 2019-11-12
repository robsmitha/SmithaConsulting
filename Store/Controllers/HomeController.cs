using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DomainLayer.Enums;
using Store.Models;
using DataLayer.Models;

namespace Store.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            var order = GetOrder();
            var model = new RegisterViewModel(order);
            return View(model);
        }
        public IActionResult List(int? orderId)
        {
            var order = GetOrder(orderId);
            var items = new List<ItemModel>();
            if (MerchantID > 0)
            {
                items = API.GetAll<ItemModel>("/items").Where(x => x.MerchantID == MerchantID && x.ItemTypeID != (int)ItemTypeEnums.Discount).ToList();
            }
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
        protected bool AddLineItem(int itemId, int? orderId = null)
        {
            var item = API.Get<ItemModel>($"/items/{itemId}");
            if (item?.ID > 0)
            {
                var lineItem = new LineItemModel
                {
                    ItemAmount = item.Price ?? 0M,
                    ItemID = item.ID,
                };
                var order = orderId > 0 ? GetOrder(orderId) : null;
                //Create Order if needed
                if (order == null)
                {
                    order = new OrderModel
                    {
                        OrderStatusTypeID = (int)OrderStatusTypeEnums.Open,
                        MerchantID = MerchantID.Value,
                        CustomerID = CustomerID
                    };

                    API.Add("/orders", order);
                }
                API.Add("/lineitems", lineItem);
                return true;
            }
            return false;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddLineItem(RegisterViewModel model)
        {
            var msg = string.Empty;
            var success = true;
            var order = await GetOrderAsync();

            if (MerchantID > 0)
            {
                var item = API.Get<ItemModel>($"/items/{model.SelectedItemID}");
                if (item != null)
                {
                    var lineItem = new LineItemModel
                    {
                        ItemAmount = item.Price.Value,
                        ItemID = item.ID,
                    };
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
                        order = API.Add("/orders", order);
                    }
                    lineItem.OrderID = order.ID;
                    API.Add("/lineitems", lineItem);
                    success = true;
                }
            }

            //if (order == null)
            //{
            //    order = new OrderDTO
            //    {
            //        OrderStatusTypeID = (int)OrderStatusTypeEnums.Open,
            //        MerchantID = MerchantID.Value,
            //        CustomerID = CustomerID,
            //        CreatedAt = DateTime.Now
            //    };
            //    order = API.Add("/orders", order);
            //}
            //var item = await API.GetAsync<ItemDTO>($"/items/{model.SelectedItemID}");
            //if (item != null)
            //{
            //    var lineItem = new LineItemDTO
            //    {
            //        ItemAmount = item.Price.Value,
            //        ItemID = item.ID,
            //    };
            //    API.Add($"/orders/{model.CurrentOrderID}/lineitems/", lineItem);
            //}
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
                var lineItem = API.GetAll<LineItemModel>("/lineitems")
                    .LastOrDefault(x => x.OrderID == order.ID && x.ItemID == model.SelectedItemID);
                if (lineItem != null)
                {
                    API.Delete($"/lineitems/{lineItem.ID}");
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
                //API.GetAll<LineItemDTO>("/lineitems")
                //    .Where(x => x.OrderID == order.ID && x.ItemID == model.SelectedItemID)
                //    .ToList()
                //    .ForEach(x => API.Delete($"/lineitems/{x.ID}"));

                API.Delete($"/orders/{order.ID}/lineitems/{model.SelectedItemID}");
                UpdateDiscounts(order);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                success = false;
            }
            return Json(new { success, msg, orderId = order?.ID });
        }
        public IActionResult Payment()
        {
            var order = GetOrder();
            if (order != null)
            {
                var orderViewModel = GetOrderViewModel(order);
                var model = new PaymentViewModel(orderViewModel, order.ID);
                return View(model);
            }
            return RedirectToAction("Error");
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
                        var customer = await API.GetAsync<CustomerModel>($"/customers/{CustomerID.Value}");
                        customer.Email = model.Email;
                        customer.FirstName = model.FirstName;
                        customer.LastName = model.LastName;
                        API.Update("/customer", customer);

                        var amount = API.GetAll<LineItemModel>("/lineitems")
                            .Where(x => x.OrderID == order.ID).Sum(x => x.ItemAmount);
                        //Create payment on order.
                        var payment = new PaymentModel
                        {
                            OrderID = model.CurrentOrderID,
                            PaymentTypeID = (int)PaymentTypeEnums.CreditCardManual,
                            Amount = amount,
                            PaymentStatusTypeID = (int)PaymentStatusTypeEnums.Paid
                        };

                        API.Add("/payments", payment);

                        order.OrderStatusTypeID = (int)OrderStatusTypeEnums.Paid;
                        API.Update($"/orders/{order.ID}", order);

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
                var discount = API.GetAll<ItemModel>("/items")
                .FirstOrDefault(x => x.ItemTypeID == (int)ItemTypeEnums.Discount && x.MerchantID == MerchantID && x.LookupCode == lookupCode);
                if (discount != null)
                {
                    var amount = 0M;
                    switch (discount.PriceTypeID)
                    {
                        case (int)PriceTypeEnums.Fixed:
                            amount = discount.Price ?? 0;
                            break;
                        case (int)PriceTypeEnums.Variable:
                            var lineItems = API.GetAll<LineItemModel>("/lineitems").Where(x => x.OrderID == order.ID);

                            amount = lineItems.Sum(x => x.ItemAmount) * discount.Percentage.Value;
                            break;
                    }
                    var lineItem = new LineItemModel
                    {
                        ItemAmount = amount > 0 ? amount * -1 : amount,
                        ItemID = discount.ID,
                        OrderID = order.ID
                    };
                    API.Add("/lineitems", lineItem);
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
                var discounts = API.GetAll<ItemModel>("/items")
                .Where(x => x.ItemTypeID == (int)ItemTypeEnums.Discount && x.MerchantID == MerchantID);
                foreach (var discount in discounts)
                {
                    var lineItemDiscount = API.Get<ItemModel>($"/items/{discount.ID}");
                    if (lineItemDiscount?.ID > 0)
                    {

                        var amount = 0M;
                        switch (discount.PriceTypeID)
                        {
                            case (int)PriceTypeEnums.Fixed: //fixed
                                amount = discount.Price ?? 0;
                                break;
                            case (int)PriceTypeEnums.Variable: //variable
                                var lineItems = API.GetAll<LineItemModel>("/lineitems").Where(x => x.OrderID == order.ID && x.ID != lineItemDiscount.ID);
                                amount = lineItems.Sum(x => x.ItemAmount) * discount.Percentage ?? 0;
                                break;
                        }

                        API.Delete($"/lineitems/{lineItemDiscount.ID}");

                        if (amount != 0)
                        {
                            var lineItem = new LineItemModel
                            {
                                ItemAmount = amount > 0 ? amount * -1 : amount,
                                ItemID = discount.ID,
                                OrderID = order.ID
                            };
                            API.Add("/lineitems", lineItem);
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
            var lineItems = API.GetAll<LineItemModel>("/lineitems").Where(x => x.OrderID == orderId && x.ItemID == itemId);
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
                var lineItems = API.GetAll<LineItemModel>("/lineitems").Where(x => x.OrderID == model.OrderID && x.ItemID == model.ItemID).ToList();
                var itemCount = lineItems.Count;
                if (itemCount > 0)
                {
                    if (model.Quantity > itemCount)
                    {
                        // add
                        for (int i = 0; i < model.Quantity - itemCount; i++)
                        {
                            success &= AddLineItem(model.ItemID, model.OrderID);
                        }
                    }
                    else if (model.Quantity < itemCount)
                    {
                        // remove
                        for (int i = 0; i < itemCount - model.Quantity && i < lineItems.Count; i++)
                        {
                            API.Delete($"/lineItems/{lineItems[i].ID}");
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
