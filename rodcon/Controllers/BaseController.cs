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
using rodcon.Constants;
using rodcon.Models;
using rodcon.Utilities;

namespace rodcon.Controllers
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
        public int? UserID => HttpContext.Session.GetInt32(SessionKeysConstants.USER_ID);
        public int? CustomerID => UserID > 0 ? null : HttpContext.Session.GetInt32(SessionKeysConstants.CUSTOMER_ID);
        public int? MerchantID => HttpContext.Session.GetInt32(SessionKeysConstants.MERCHANT_ID);
        public string Username => HttpContext.Session.GetString(SessionKeysConstants.USERNAME);
        public string ThemeCDN => HttpContext.Session.GetString(SessionKeysConstants.THEME_CDN);

        #region Permissions
        public List<Permission> UserPermissions
        {
            get
            {
                var permissions = new List<Permission>();
                var permissionsString = HttpContext.Session.GetString(SessionKeysConstants.PERMISSION_LIST);
                if (!string.IsNullOrEmpty(permissionsString))
                {
                    permissions = JsonConvert.DeserializeObject<List<Permission>>(permissionsString);
                }
                return permissions;
            }
        }
        public bool CheckPermission(string permissionName = null, string controller = null, string action = "Index")
        {
            return !string.IsNullOrEmpty(permissionName)
                ? UserPermissions.Any(x => x.Name == permissionName)
                : UserPermissions
                .Where(x => !string.IsNullOrEmpty(x.Controller) && !string.IsNullOrEmpty(x.Action))
                .Any(x => x.Controller == controller && x.Action == action);
        }
        #endregion

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                //TODO: Handle session timeout
                return;
            }

            var action = ControllerContext.RouteData.Values["action"].ToString().ToLower();
            var controller = ControllerContext.RouteData.Values["controller"].ToString().ToLower();
            string[] publicPages = { "login", "loginasync", "signout", "signup", "signupasync" };

            if(controller == "home" && Array.IndexOf(publicPages, action) != -1)
            {
                //this is a public page
                return;
            }
            else
            {
                if(UserID > 0)
                {
                    var controllerActionPermissions = _context.Permissions.Where(x => x.Controller == controller && x.Action == action);

                    if (controllerActionPermissions != null)
                    {
                        //this controller action has some required permissions
                        foreach (var controllerActionPermission in controllerActionPermissions)
                        {
                            if (!UserPermissions.Select(x => x.ID).Contains(controllerActionPermission.ID))
                            {
                                //TODO: permission denied message
                                context.Result = new RedirectResult("/Home/Index");
                                return;
                            }
                        }
                    }
                }
                else
                {
                    context.Result = new RedirectResult("/Home/Login");
                }          
            }
        }
        public void GenerateRandomTheme()
        {
            if(ThemeCDN == null)
            {
                var themeModel = GetThemeList();
                if (themeModel?.Result != null && themeModel.Result?.themes != null)
                {
                    var themeList = themeModel.Result.themes.ToList();
                    Random rnd = new Random();
                    int themeIndex = rnd.Next(1, themeList.Count());
                    var theme = themeList[themeIndex];
                    HttpContext.Session.SetString(SessionKeysConstants.THEME_CDN, theme?.cssCdn);
                }
            }
        }

        public void CreateUserSession(User user)
        {
            var merchantId = _context.MerchantUsers.FirstOrDefault(x => x.UserID == user.ID)?.MerchantID;
            if (merchantId != null)
            {
                HttpContext.Session.SetInt32(SessionKeysConstants.MERCHANT_ID, merchantId.Value);
            }
            var userId = user.ID;
            var username = user.Username;
            HttpContext.Session.SetInt32(SessionKeysConstants.USER_ID, userId);
            HttpContext.Session.SetString(SessionKeysConstants.USERNAME, username);
            var merchantUser = _context.MerchantUsers.SingleOrDefault(x => x.UserID == userId && x.MerchantID == merchantId);
            if(merchantUser != null)
            {
                var rolePermissions = _context.RolePermissions.Where(x => x.RoleID == merchantUser.RoleID);
                var permissionIds = rolePermissions.Select(x => x.PermissionID);
                var permissions = _context.Permissions.Where(x => permissionIds.Contains(x.ID));
                if (permissions.Any())
                {
                    HttpContext.Session.SetString(SessionKeysConstants.PERMISSION_LIST, JsonConvert.SerializeObject(permissions));
                }
            }
        }
        public bool HasPermission(int permissionId)
        {
            var permissionIdString = HttpContext.Session.GetString(SessionKeysConstants.PERMISSION_LIST);
            if (!string.IsNullOrEmpty(permissionIdString))
            {
                var permissionIds = JsonConvert.DeserializeObject<List<int>>(permissionIdString);
                return permissionIds.Contains(permissionId);
            }
            return false;
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
                .Where(x => x.UserID == UserID && x.CustomerID == CustomerID && x.OrderStatusTypeID == (int)OrderStatusTypeEnums.Open)
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

        public async Task<ThemeListViewModel> GetThemeList()
        {
            var model = new ThemeListViewModel();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ThemeViewModel.BaseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync(ThemeViewModel.BaseUrl);
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;  
                    model = JsonConvert.DeserializeObject<ThemeListViewModel>(result);
                    model.CurrentTheme = ThemeCDN;
                }
                return model;
            }
        }
    }
}