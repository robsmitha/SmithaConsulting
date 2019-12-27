using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using DomainLayer.Enums;
using Store.Constants;
using Store.Models;
using Store.Utilities;
using DomainLayer.Models;
using DomainLayer.Services;
using AutoMapper;

namespace Store.Controllers
{
    public class BaseController : Controller
    {
        public string ApplicationName => ConfigurationManager.GetConfiguration("ApplicationName");

        #region CDN
        public string CDNLocation => ConfigurationManager.GetConfiguration("AWSCDN");
        public string BucketName => ConfigurationManager.GetConfiguration("S3BucketName");
        #endregion

        #region Session
        public int? CustomerID => HttpContext.Session.GetInt32(SessionKeysConstants.CUSTOMER_ID);
        public int? MerchantID => HttpContext.Session.GetInt32(SessionKeysConstants.MERCHANT_ID);
        public string ThemeCDN => HttpContext.Session.GetString(SessionKeysConstants.THEME_CDN);
        #endregion

        protected readonly IApiService _api;
        protected readonly IMapper _mapper;
        protected readonly ICacheService _cache;
        public BaseController(IApiService api, IMapper mapper, ICacheService cache)
        {
            _api = api;
            _mapper = mapper;
            _cache = cache;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                //TODO: Handle session timeout
                return;
            }

            if (CustomerID == null)
            {
                var customer = _api.Post("/customers", new CustomerModel());
                CreateCustomerSession(customer);
            }

            #region Set Theme in Session
            if (ThemeCDN == null && !string.IsNullOrEmpty(ApplicationName))
            {
                var application = _api.Get<ApplicationModel>($"/applications/GetByName/{ApplicationName}");
                if (application != null)
                {
                    var theme = _api.Get<ThemeModel>($"/themes/{application.ThemeID}");
                    if (theme != null)
                    {
                        HttpContext.Session.SetString(SessionKeysConstants.THEME_CDN, theme?.StyleSheetCDN);
                    }
                }
            }
            #endregion
        }

        public void CreateCustomerSession(CustomerModel customer)
        {
            HttpContext.Session.SetInt32(SessionKeysConstants.CUSTOMER_ID, customer.ID);
            var merchantId = _api.Get<IEnumerable<MerchantModel>>("/merchants").FirstOrDefault(x => x.MerchantTypeID == (int)MerchantTypeEnums.Online && x.Active)?.ID;
            if (merchantId != null)
            {
                HttpContext.Session.SetInt32(SessionKeysConstants.MERCHANT_ID, merchantId.Value);
            }
        }

        #region Order helper methods
        public OrderModel GetOrder(int? orderId = null)
        {
            if (orderId > 0)
            {
                var order = _api.Get<OrderModel>($"/orders/{orderId}");
                if(order.CustomerID != CustomerID)
                {
                    return null;
                }
                return order;
            }
            return CustomerID > 0
                ? _api.Get<IEnumerable<OrderModel>>($"customers/{CustomerID}/orders").LastOrDefault(x => x.OrderStatusTypeID == (int)OrderStatusTypeEnums.Open)
                : null; 
        }

        public async Task<OrderModel> GetOrderAsync(int? orderId = null)
        {
            if (orderId > 0)
            {
                var order = await _api.GetAsync<OrderModel>($"/orders/{orderId}");
                if (order.CustomerID != CustomerID)
                {
                    return null;
                }
                return order;
            }
            if(CustomerID > 0)
            {
                var a = await _api.GetAsync<IEnumerable<OrderModel>>($"customers/{CustomerID}/orders");
                return a.LastOrDefault(x => x.OrderStatusTypeID == (int)OrderStatusTypeEnums.Open);
            }
            return null;
        }

        public OrderViewModel GetOrderViewModel(OrderModel order) => new OrderViewModel(order);
        #endregion

    }
}