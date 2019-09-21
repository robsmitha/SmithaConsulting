using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using rod;
using rod.Data;
using rod.Enums;
using store.Constants;
using store.Models;
using store.Utilities;

namespace store.Controllers
{
    public class BaseController : Controller
    {
        private readonly rodContext _context;

        public BaseController(rodContext context)
        {
            _context = context;
        }
        public string CDNLocation => ConfigurationManager.GetConfiguration("AWSCDN");
        public string BucketName => ConfigurationManager.GetConfiguration("S3BucketName");
        public string ConversationStirng => HttpContext.Session.GetString("Conversation") ?? string.Empty;
        public bool HasSessionConversation => !string.IsNullOrEmpty(ConversationStirng);
        public int? CustomerID => HttpContext.Session.GetInt32(SessionKeysConstants.CUSTOMER_ID);
        public int? MerchantID => HttpContext.Session.GetInt32(SessionKeysConstants.MERCHANT_ID);
        public string ThemeCDN => HttpContext.Session.GetString(SessionKeysConstants.THEME_CDN);
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                //TODO: Handle session timeout
                return;
            }

            if (CustomerID == null)
            {
                var customer = new Customer();
                _context.Customers.Add(customer);
                _context.SaveChanges();
                CreateCustomerSession(customer);
            }

            var actionName = ControllerContext.RouteData.Values["action"].ToString().ToLower();
            var controllerName = ControllerContext.RouteData.Values["controller"].ToString().ToLower();
            
            //context.Result = new RedirectResult("/Home/Login");
            return;

        }

        public void CreateCustomerSession(Customer customer)
        {
            HttpContext.Session.SetInt32(SessionKeysConstants.CUSTOMER_ID, customer.ID);
            var merchantId = _context.Merchants.FirstOrDefault(x => x.MerchantTypeID == (int)MerchantTypeEnums.Online && x.Active)?.ID;
            if (merchantId != null)
            {
                HttpContext.Session.SetInt32(SessionKeysConstants.MERCHANT_ID, merchantId.Value);
            }
        }

        public Order GetOrder(int? orderId = null)
        {
            if(orderId != null)
            {
                return _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Merchant)
                .Include(o => o.OrderStatusType)
                .Include(o => o.User)
                .SingleOrDefault(x => x.ID == orderId);
            }
            return _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Merchant)
                .Include(o => o.OrderStatusType)
                .Include(o => o.User)
                .Where(x => x.CustomerID == CustomerID && x.OrderStatusTypeID == (int)OrderStatusTypeEnums.Open)
                .OrderByDescending(x => x.CreatedAt)
                .FirstOrDefault();
        }

        public OrderViewModel GetOrderViewModel(Order order)
        {
            var payments = new List<Payment>();
            var lineItems = new List<LineItem>();
            if (order != null)
            {
                payments = _context.Payments
                    .Include(p => p.Authorization)
                    .Include(p => p.PaymentStatusType)
                    .Include(p => p.PaymentType)
                    .Where(x => x.OrderID == order.ID)
                   .ToList();
                lineItems = _context.LineItems.Include(l => l.Item).Where(x => x.OrderID == order.ID)
                .ToList();
            }
            return new OrderViewModel(order, lineItems, payments);
        }

    }
}