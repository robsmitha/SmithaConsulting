using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using rod;
using rod.Data;
using rod.Enums;
using rodcon.Models;

namespace rodcon.Controllers
{
    public class RegisterController : BaseController
    {
        private readonly rodContext _context;
        public RegisterController(rodContext context)
        {
            _context = context;
        }
        public IActionResult Index(int? orderId)
        {
            Order order = null;
            OrderViewModel orderViewModel = null;
            var items = new List<Item>();
            if (MerchantID > 0)
            {
                var payments = new List<Payment>();
                var lineItems = new List<LineItem>();
                items = _context.Items.Where(x => x.MerchantID == MerchantID).ToList();
                order = orderId != null
                    ? _context.Orders.SingleOrDefault(x => x.ID == orderId)     //get passed order
                    : _context.Orders.FirstOrDefault(x => x.UserID == UserID && x.OrderStatusTypeID == (int)OrderStatusTypeEnums.Open);  //get users last pending order
                if (order != null)
                {
                    payments = _context.Payments.Where(x => x.OrderID == order.ID)
                       .ToList();
                    lineItems = _context.LineItems.Where(x => x.OrderID == order.ID)
                    .ToList();
                }
                orderViewModel = new OrderViewModel(order, lineItems, payments);
            }

            var model = new RegisterViewModel(items, orderViewModel);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddLineItem(RegisterViewModel model)
        {
            User user = null;
            Order order = null;
            if (UserID > 0 && MerchantID > 0)
            {
                var item = _context.Items.SingleOrDefault(x => x.ID == model.SelectedItemID);
                if(item != null)
                {
                    user = _context.Users.SingleOrDefault(x => x.ID == UserID);
                    order = _context.Orders.SingleOrDefault(x => x.ID == model.CurrentOrderID);
                    var lineItem = new LineItem
                    {
                        ItemAmount = item.Price.Value,
                        ItemID = item.ID,
                    };
                    if (order == null)
                    {
                        order = new Order
                        {
                            OrderStatusTypeID = (int)OrderStatusTypeEnums.Open,
                            MerchantID = MerchantID.Value,
                            UserID = user.ID,
                            //TODO: add customerid
                        };
                        _context.Orders.Add(order);
                        _context.SaveChanges(); //generate orderId
                    }
                    lineItem.OrderID = order.ID;
                    _context.LineItems.Add(lineItem);
                    _context.SaveChanges(); //save order
                }
            }
            return RedirectToAction("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveLineItem(RegisterViewModel model)
        {
            var lineItem = _context.LineItems.SingleOrDefault(x => x.ID == model.SelectedLineItemID);
            if(lineItem != null)
            {
                var orderId = lineItem.OrderID;
                _context.LineItems.Remove(lineItem);
                _context.SaveChanges();
                var lineItems = _context.LineItems.Where(x => x.OrderID == orderId);
                var discounts = lineItems.Where(x => x.Item.ItemTypeID == (int)ItemTypeEnums.Discount && x.ID != lineItem.ID);
                var order = _context.Orders.SingleOrDefault(x => x.ID == orderId);
                UpdateDiscounts(order);
            }
            return RedirectToAction("Index");
        }
        public IActionResult Payment(int orderId)
        {
            var order = _context.Orders.SingleOrDefault(x => x.ID == orderId);
            if(order != null)
            {
                var payments = _context.Payments.Where(x => x.OrderID == order.ID)
                    .ToList();
                var lineItems = _context.LineItems.Where(x => x.OrderID == order.ID)
                    .ToList();
                var lineItemIds = _context.LineItems.Where(x => x.OrderID == order.ID)
                    .Select(x => x.ItemID);
                var items = _context.Items.Where(x => lineItemIds.Contains(x.ID)).ToList();
                var orderViewModel = new OrderViewModel(order, lineItems, payments);
                var model = new PaymentViewModel(orderViewModel, order.ID);
                return View(model);

            }
            return RedirectToAction("Error");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Payment(PaymentViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var order = _context.Orders.SingleOrDefault(x => x.ID == model.CurrentOrderID);
                    if(order != null)
                    {
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
                        _context.Payments.Add(payment);
                        _context.Orders.Update(order);
                        _context.SaveChanges();
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
                var amount = 0M;
                switch (discount.PriceTypeID)
                {
                    case (int)PriceTypeEnums.Fixed: //fixed
                        if (discount.Price == null) return false;
                        amount = discount.Price.Value;
                        break;
                    case (int)PriceTypeEnums.Variable: //variable
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
            catch
            {
                return false;
            }

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
                                amount = discount.Price.Value;
                                break;
                            case (int)PriceTypeEnums.Variable: //variable
                                var lineItems = _context.LineItems.Where(x => x.OrderID == order.ID && x.ID != lineItemDiscount.ID);
                                amount = lineItems.Sum(x => x.ItemAmount) * discount.Percentage.Value;
                                break;
                        }

                        var lineItem = new LineItem
                        {
                            ItemAmount = amount > 0 ? amount * -1 : amount,
                            ItemID = discount.ID,
                            OrderID = order.ID
                        };

                        _context.LineItems.Remove(lineItemDiscount);
                        _context.LineItems.Add(lineItem);
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
                return RedirectToAction("Payment", new { orderId = model.CurrentOrderID });
            }
            return RedirectToAction("Error");
        }
    }
}