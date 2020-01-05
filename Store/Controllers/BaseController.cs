using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Domain.Enums;
using Store.Constants;
using Store.Models;
using Store.Utilities;
using Domain.Models;
using Domain.Services;
using AutoMapper;
using System;

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
        public int? UserID => HttpContext.Session.GetInt32(SessionKeysConstants.USER_ID);
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

            if (CustomerID <= 0 || CustomerID == null)
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
        #endregion

        public void CreateUserSession(UserModel user)
        {
            //var merchantId = _context.MerchantUsers.FirstOrDefault(x => x.UserID == user.ID)?.MerchantID;
            //if (merchantId != null)
            //{
            //    HttpContext.Session.SetInt32(SessionKeysConstants.MERCHANT_ID, merchantId.Value);
            //}

            HttpContext.Session.SetInt32(SessionKeysConstants.USER_ID, user.ID);
            HttpContext.Session.SetString(SessionKeysConstants.USERNAME, user.Username);
            HttpContext.Session.SetString(SessionKeysConstants.IMAGE_URL, user.ImageUrl ?? string.Empty);
            //var merchantUser = _context.MerchantUsers.SingleOrDefault(x => x.UserID == userId && x.MerchantID == merchantId);
            //if (merchantUser != null)
            //{
            //    var rolePermissions = _context.RolePermissions.Where(x => x.RoleID == merchantUser.RoleID);
            //    var permissionIds = rolePermissions.Select(x => x.PermissionID);
            //    var permissions = _context.Permissions.Where(x => permissionIds.Contains(x.ID));
            //    if (permissions.Any())
            //    {
            //        HttpContext.Session.SetString(SessionKeysConstants.PERMISSION_LIST, JsonConvert.SerializeObject(permissions));
            //    }
            //}
        }

    }
}