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
    public class PaymentsController : BaseController
    {
        public PaymentsController(IApiService api, IMapper mapper, ICacheService cache) : base(api, mapper, cache) { }
        public IActionResult Index()
        {
            return NotFound();
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
                        _api.Put($"/customers/{customer.ID}", customer);

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

                        return RedirectToAction("Order", "Account", new { id = model.CurrentOrderID });
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
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApplyDiscount(PaymentViewModel model)
        {
            var order = await GetOrderAsync(model.CurrentOrderID);
            if (AddDiscount(order, model.PromoCode))
            {

            }
            return RedirectToAction("Payment", "Payments");
        }
    }
}