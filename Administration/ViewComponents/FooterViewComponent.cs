using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Architecture;
using Architecture.Enums;
using Administration.Constants;
using Administration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Administration.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var userId = HttpContext.Session?.GetInt32("userId");
            var username = HttpContext.Session?.GetString("username");
            var @namespace = "rod";



            var entities = AppDomain.CurrentDomain.GetAssemblies()
                       .SelectMany(t => t.GetTypes())
                       .Where(t => t.IsClass && t.Namespace == @namespace);
            var entityList = new List<string>();
            foreach(var entity in entities)
            {
                entityList.Add(entity.Name);
            }

            var footer = new FooterViewModel(userId, username)
            {
                CanAccessFeatures = HasPermission((int)PermissionEnums.CanAccessFeatures),
                CanAccessTypes = HasPermission((int)PermissionEnums.CanAccessTypes),
                Entities = entityList
            };
            return View("Footer", footer);
        }
        public bool HasPermission(int permissionId)
        {
            var permissionIdString = HttpContext.Session.GetString(SessionKeysConstants.PERMISSION_LIST);
            if (!string.IsNullOrEmpty(permissionIdString))
            {
                var permissions = JsonConvert.DeserializeObject<List<Permission>>(permissionIdString);
                return permissions.Select(x => x.ID).Contains(permissionId);
            }
            return false;
        }
        public string GetSimpleTypeName(Type propertyInfo)
        {
            var simpleName = "unknown";
            var propertyTypeString = propertyInfo.ToString();

            if (propertyTypeString.Contains("System."))
            {
                simpleName = propertyTypeString.Replace("System.", "");
                if (simpleName.Contains("Nullable`1["))
                {
                    simpleName = simpleName.Replace("Nullable`1[", "");
                    simpleName = simpleName.Replace("]", "");
                    simpleName = $"{simpleName}?";
                }
            }

            return simpleName;
        }
    }
}
