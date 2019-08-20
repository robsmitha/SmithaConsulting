using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using rod;
using rod.Data;
using rodcon.Constants;

namespace rodcon.Controllers
{
    public class BaseController : Controller
    {
        public int? UserID => HttpContext.Session.GetInt32(SessionKeysConstants.USER_ID);
        public int? MerchantID => HttpContext.Session.GetInt32(SessionKeysConstants.MERCHANT_ID);
        public string Username => HttpContext.Session.GetString(SessionKeysConstants.USERNAME);
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var actionName = ControllerContext.RouteData.Values["action"].ToString().ToLower();
            var controllerName = ControllerContext.RouteData.Values["controller"].ToString().ToLower();
            if (UserID == null || UserID == 0)
            {
                string[] publicPages = { "index", "login", "loginasync", "signout", "signup", "signupasync", "about", "privacy", "content" };
                if (controllerName == "home" && Array.IndexOf(publicPages, actionName) != -1)
                {
                    return;
                }
                else
                {
                    context.Result = new RedirectResult("/Home/Login");
                    return;
                }
            }
        }
        public void CreateUserSession(rodContext context, User user)
        {
            var merchantId = context.MerchantUsers.FirstOrDefault(x => x.UserID == user.ID)?.MerchantID;
            if (merchantId != null)
            {
                HttpContext.Session.SetInt32(SessionKeysConstants.MERCHANT_ID, merchantId.Value);
            }
            var userId = user.ID;
            var username = user.Username;
            HttpContext.Session.SetInt32(SessionKeysConstants.USER_ID, userId);
            HttpContext.Session.SetString(SessionKeysConstants.USERNAME, username);
            var merchantUser = context.MerchantUsers.SingleOrDefault(x => x.UserID == userId && x.MerchantID == merchantId);
            if(merchantUser != null)
            {
                var rolePermissions = context.RolePermissions.Where(x => x.RoleID == merchantUser.RoleID);
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
    }
}