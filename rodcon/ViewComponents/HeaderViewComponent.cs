using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using rod;
using rod.Data;
using rod.Enums;
using rodcon.Constants;
using rodcon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rodcon.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var userId = HttpContext.Session?.GetInt32("userId");
            var username = HttpContext.Session?.GetString("username");
            var header = new HeaderViewModel(userId, username)
            {
                CanAccessFeatures = HasPermission((int)PermissionEnums.CanAccessFeatures),
                CanAccessTypes = HasPermission((int)PermissionEnums.CanAccessTypes)
            };
            return View("Header", header);
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
