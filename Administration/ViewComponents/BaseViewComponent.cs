using Administration.Constants;
using Architecture;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Administration.ViewComponents
{
    public abstract class BaseViewComponent : ViewComponent
    {
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
    }
}
