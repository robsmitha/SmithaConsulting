using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataModeling;
using DataModeling.Data;
using Architecture.Enums;
using Administration.Models;
using Microsoft.EntityFrameworkCore;

namespace Administration.Controllers
{
    public class RegisterController : BaseController
    {
        private readonly DbArchitecture _context;
        public RegisterController(DbArchitecture context) : base(context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            Order order = GetOrder();
            var model = new RegisterViewModel(order);
            return View(model);
        }
        public IActionResult List(int? orderId)
        {
            Order order = GetOrder(orderId);
            var items = new List<Item>();
            if (MerchantID > 0)
            {
                items = _context.Items.Where(x => x.MerchantID == MerchantID && x.ItemTypeID != (int)ItemTypeEnums.Discount).ToList();
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
            var item = _context.Items.SingleOrDefault(x => x.ID == itemId);
            if (item != null)
            {
                var lineItem = new LineItem
                {
                    ItemAmount = item.Price ?? 0M,
                    ItemID = item.ID,
                };
                //Create Order if needed
                var order = _context.Orders.SingleOrDefault(x => x.ID == orderId);
                if (order == null)
                {
                    order = new Order
                    {
                        OrderStatusTypeID = (int)OrderStatusTypeEnums.Open,
                        MerchantID = MerchantID.Value,
                        UserID = UserID,
                        CustomerID = CustomerID
                    };
                    _context.Orders.Add(order);
                    _context.SaveChanges();
                }
                lineItem.OrderID = order.ID;
                _context.LineItems.Add(lineItem);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddLineItem(RegisterViewModel model)
        {
            var msg = string.Empty;
            var success = true;
            var order = GetOrder(model.CurrentOrderID);
            if (MerchantID > 0)
            {
                var item = _context.Items.SingleOrDefault(x => x.ID == model.SelectedItemID);
                if (item != null)
                {
                    var lineItem = new LineItem
                    {
                        ItemAmount = item.Price.Value,
                        ItemID = item.ID,
                    };
                    //Create Order if needed
                    if (order == null)
                    {
                        order = new Order
                        {
                            OrderStatusTypeID = (int)OrderStatusTypeEnums.Open,
                            MerchantID = MerchantID.Value,
                            UserID = UserID,
                            CustomerID = CustomerID
                        };
                        _context.Orders.Add(order);
                        _context.SaveChanges();
                    }
                    lineItem.OrderID = order.ID;

                    _context.LineItems.Add(lineItem);
                    _context.SaveChanges();
                    success = true;
                }
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
                var lineItem = _context.LineItems.LastOrDefault(x => x.OrderID == order.ID && x.ItemID == model.SelectedItemID);
                if (lineItem != null)
                {
                    _context.LineItems.Remove(lineItem);
                    _context.SaveChanges();
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
                var lineItems = _context.LineItems.Where(x => x.OrderID == order.ID && x.ItemID == model.SelectedItemID);
                if (lineItems != null)
                {
                    _context.LineItems.RemoveRange(lineItems);
                    _context.SaveChanges();
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
        public IActionResult Payment()
        {
            var order = GetOrder();
            if(order != null)
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
                if (ModelState.IsValid)
                {
                    var order = _context.Orders.SingleOrDefault(x => x.ID == model.CurrentOrderID);
                    if(order != null)
                    {
                        if (!string.IsNullOrWhiteSpace(model.Email))
                        {

                            var customer = new Customer();
                            customer.Email = model.Email;
                            customer.FirstName = model.FirstName;
                            customer.LastName = model.LastName;
                            customer.Active = true;
                            customer.CreatedAt = DateTime.Now;
                            await _context.AddAsync(customer);
                            await _context.SaveChangesAsync();
                            order.CustomerID = customer.ID;
                        }

                        var amount = _context.LineItems.Where(x => x.OrderID == order.ID).Sum(x => x.ItemAmount);
                        //Create payment on order.
                        var payment = new Payment
                        {
                            OrderID = model.CurrentOrderID,
                            PaymentTypeID = (int)PaymentTypeEnums.CreditCardManual,
                            Amount = amount,
                            PaymentStatusTypeID = (int)PaymentStatusTypeEnums.Paid,
                            Active = true,
                            CreatedAt = DateTime.Now
                        };
                        order.OrderStatusTypeID = (int)OrderStatusTypeEnums.Paid;
                        await _context.AddAsync(payment);
                        _context.Update(order);
                        await _context.SaveChangesAsync();
                        return RedirectToAction("Details", "Orders", new { id = model.CurrentOrderID });
                    }
                }
                return View(model);
            }
            catch
            {
                return RedirectToAction("Error");
            }
            
        }
        protected bool AddDiscount(Order order, string lookupCode)
        {
            try
            {
                var discount = _context.Items.FirstOrDefault(x => x.ItemTypeID == (int)ItemTypeEnums.Discount && x.MerchantID == MerchantID && x.LookupCode == lookupCode);
                if(discount != null)
                {
                    var amount = 0M;
                    switch (discount.PriceTypeID)
                    {
                        case (int)PriceTypeEnums.Fixed:
                            amount = discount.Price ?? 0;
                            break;
                        case (int)PriceTypeEnums.Variable:
                            var lineItems = _context.LineItems.Where(x => x.OrderID == order.ID);
                            amount = lineItems.Sum(x => x.ItemAmount) * discount.Percentage.Value;
                            break;
                    }
                    var lineItem = new LineItem
                    {
                        ItemAmount = amount > 0 ? amount * -1 : amount,
                        ItemID = discount.ID,
                        OrderID = order.ID
                    };
                    _context.LineItems.Add(lineItem);
                    _context.SaveChanges();
                    return true;
                }
            }
            catch
            {

            }
            return false;

        }
        protected bool UpdateDiscounts(Order order)
        {
            try
            {
                var discounts = _context.Items.Where(x => x.ItemTypeID == (int)ItemTypeEnums.Discount && x.MerchantID == MerchantID);
                foreach (var discount in discounts)
                {
                    var lineItemDiscount = _context.LineItems.SingleOrDefault(x => x.ItemID == discount.ID);
                    if(lineItemDiscount != null)
                    {

                        var amount = 0M;
                        switch (discount.PriceTypeID)
                        {
                            case (int)PriceTypeEnums.Fixed: //fixed
                                amount = discount.Price ?? 0;
                                break;
                            case (int)PriceTypeEnums.Variable: //variable
                                var lineItems = _context.LineItems.Where(x => x.OrderID == order.ID && x.ID != lineItemDiscount.ID);
                                amount = lineItems.Sum(x => x.ItemAmount) * discount.Percentage ?? 0;
                                break;
                        }
                        _context.LineItems.Remove(lineItemDiscount);
                        if (amount != 0)
                        {
                            var lineItem = new LineItem
                            {
                                ItemAmount = amount > 0 ? amount * -1 : amount,
                                ItemID = discount.ID,
                                OrderID = order.ID
                            };
                            _context.LineItems.Add(lineItem);
                        }
                    }
                }
                _context.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ApplyDiscount(PaymentViewModel model)
        {
            var order = _context.Orders.SingleOrDefault(x => x.ID == model.CurrentOrderID);
            if (AddDiscount(order, model.PromoCode))
            {

            }
            return RedirectToAction("Payment", new { orderId = model.CurrentOrderID });
        }
        public IActionResult Edit(int itemId, int orderId)
        {
            var model = new RegisterEditViewModel();
            var lineItems = _context.LineItems.Where(x => x.OrderID == orderId && x.ItemID == itemId);
            var lineItem = lineItems.FirstOrDefault();
            if(lineItem != null)
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
                var lineItems = _context.LineItems.Where(x => x.OrderID == model.OrderID && x.ItemID == model.ItemID).ToList();

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
                            _context.LineItems.Remove(lineItems[i]);
                        }
                        _context.SaveChanges();
                    }
                }
                
            }
            catch(Exception ex)
            {
                msg = ex.Message;
                success = false;
            }
            return Json(new { success, msg, orderId = model.OrderID });
        }
    }
}