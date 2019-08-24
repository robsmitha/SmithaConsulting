using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using rod;
using rod.Data;
using rod.Enums;
using rodcon.Constants;
using rodcon.Models;

namespace rodcon.Controllers
{
    public class BaseController : Controller
    {
        private readonly rodContext _context;

        public BaseController(rodContext context)
        {
            _context = context;
        }
        public int? UserID => HttpContext.Session.GetInt32(SessionKeysConstants.USER_ID);
        public int? CustomerID => UserID > 0 ? null : HttpContext.Session.GetInt32(SessionKeysConstants.CUSTOMER_ID);
        public int? MerchantID => HttpContext.Session.GetInt32(SessionKeysConstants.MERCHANT_ID);
        public string Username => HttpContext.Session.GetString(SessionKeysConstants.USERNAME);
        public string ThemeCDN => HttpContext.Session.GetString(SessionKeysConstants.THEME_CDN);
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                //TODO: Handle session timeout
                return;
            }

            //if(ThemeCDN == null)
            //{
            //    var themeModel = GetThemeList();
            //    if (themeModel?.Result != null && themeModel.Result?.themes != null)
            //    {
            //        var themeList = themeModel.Result.themes.ToList();
            //        Random rnd = new Random();
            //        int themeIndex = rnd.Next(1, themeList.Count());
            //        var theme = themeList[themeIndex];
            //        HttpContext.Session.SetString(SessionKeysConstants.THEME_CDN, theme?.cssCdn);
            //    }
            //}

            if (UserID == null && CustomerID == null)
            {
                var customer = new Customer();
                _context.Customers.Add(customer);
                _context.SaveChanges();
                CreateCustomerSession(customer);
            }
            else if(UserID > 0)
            {
                return;
            }
            var actionName = ControllerContext.RouteData.Values["action"].ToString().ToLower();
            var controllerName = ControllerContext.RouteData.Values["controller"].ToString().ToLower();
            string[] publicPages = { "index", "login", "loginasync", "signout", "signup", "signupasync", "about", "privacy", "contact", "payment", "details", "apply", "error" };
            switch (controllerName)
            {
                case "home":
                case "register":
                case "orders":
                case "theme":
                    if (Array.IndexOf(publicPages, actionName) != -1) return;
                    break;
            }
            context.Result = new RedirectResult("/Home/Login");
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
                if (permissionIds.Any())
                {
                    HttpContext.Session.SetString(SessionKeysConstants.PERMISSION_ID_LIST, JsonConvert.SerializeObject(permissionIds));
                }
            }
        }
        public bool HasPermission(int permissionId)
        {
            var permissionIdString = HttpContext.Session.GetString(SessionKeysConstants.PERMISSION_ID_LIST);
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
                return _context.Orders.SingleOrDefault(x => x.ID == orderId);
            }
            return _context.Orders.Where(x => x.UserID == UserID && x.CustomerID == CustomerID && x.OrderStatusTypeID == (int)OrderStatusTypeEnums.Open)
                    .OrderByDescending(x => x.CreatedAt)
                    .FirstOrDefault();
        }

        public async Task<ThemeListViewModel> GetThemeList()
        {
            var model = new ThemeListViewModel();

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(ThemeViewModel.BaseUrl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage Res = await client.GetAsync(ThemeViewModel.BaseUrl);

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var ThemeListResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the list  
                    model = JsonConvert.DeserializeObject<ThemeListViewModel>(ThemeListResponse);
                    model.CurrentTheme = ThemeCDN;

                }
                //returning the list to view  
                return model;
            }
        }
    }
}