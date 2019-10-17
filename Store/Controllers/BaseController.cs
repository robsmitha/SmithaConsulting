using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Architecture.Enums;
using Store.Constants;
using Store.Models;
using Store.Utilities;
using Architecture.DTOs;
using Architecture.Utilities;

namespace Store.Controllers
{
    public class BaseController : Controller
    {
        public int? ApplicationID => int.TryParse(ConfigurationManager.GetConfiguration("ApplicationID"), out var @int) ? (int?)@int : null;

        #region CDN
        public string CDNLocation => ConfigurationManager.GetConfiguration("AWSCDN");
        public string BucketName => ConfigurationManager.GetConfiguration("S3BucketName");
        #endregion

        #region Session
        public int? CustomerID => HttpContext.Session.GetInt32(SessionKeysConstants.CUSTOMER_ID);
        public int? MerchantID => HttpContext.Session.GetInt32(SessionKeysConstants.MERCHANT_ID);
        public string ThemeCDN => HttpContext.Session.GetString(SessionKeysConstants.THEME_CDN);
        #endregion

        #region API
        public static string APIEndpoint = ConfigurationManager.GetConfiguration("APIEndpoint");
        public static string APIKey = "key";// ConfigurationManager.GetConfiguration("APIKey");
        public APIExtensions API = new APIExtensions(APIEndpoint, APIKey);
        #endregion

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                //TODO: Handle session timeout
                return;
            }

            if (CustomerID == null)
            {
                var customer = API.Add("/customers", new CustomerDTO());
                CreateCustomerSession(customer);
            }

            #region Set Theme in Session
            if (ThemeCDN == null && ApplicationID > 0)
            {
                var application = API.Get<ApplicationDTO>($"/applications/{ApplicationID}");
                if (application != null)
                {
                    var theme = API.Get<ThemeDTO>($"/themes/{application.ThemeID}");
                    if (theme != null)
                    {
                        HttpContext.Session.SetString(SessionKeysConstants.THEME_CDN, theme?.StyleSheetCDN);
                    }
                }
            }
            #endregion
        }

        public void CreateCustomerSession(CustomerDTO customer)
        {
            HttpContext.Session.SetInt32(SessionKeysConstants.CUSTOMER_ID, customer.ID);
            var merchantId = API.GetAll<MerchantDTO>("/merchants").FirstOrDefault(x => x.MerchantTypeID == (int)MerchantTypeEnums.Online && x.Active)?.ID;
            if (merchantId != null)
            {
                HttpContext.Session.SetInt32(SessionKeysConstants.MERCHANT_ID, merchantId.Value);
            }
        }

        #region Order helper methods
        public OrderDTO GetOrder(int? orderId = null)
        {
            if (orderId != null)
            {
                var order = API.Get<OrderDTO>($"/orders/{orderId}");
                return order;
            }
            return API.GetAll<OrderDTO>("/orders")
                .LastOrDefault(x => x.CustomerID == CustomerID && x.OrderStatusTypeID == (int)OrderStatusTypeEnums.Open);
        }

        public async Task<OrderDTO> GetOrderAsync(int? orderId = null)
        {
            if (orderId != null)
            {
                return await API.GetAsync<OrderDTO>($"/orders/{orderId}");
            }
            return API.GetAll<OrderDTO>("/orders")
                .LastOrDefault(x => x.CustomerID == CustomerID && x.OrderStatusTypeID == (int)OrderStatusTypeEnums.Open);
        }

        public OrderViewModel GetOrderViewModel(OrderDTO order)
        {
            var payments = new List<PaymentDTO>();
            var lineItems = new List<LineItemDTO>();
            if (order != null)
            {
                payments = API.GetAll<PaymentDTO>("/payments")
                    .Where(x => x.OrderID == order.ID)
                   .ToList();
                lineItems = API.GetAll<LineItemDTO>("/lineitems").Where(x => x.OrderID == order.ID)
                .ToList();
            }
            return new OrderViewModel(order, lineItems, payments);
        }
        #endregion

    }
}