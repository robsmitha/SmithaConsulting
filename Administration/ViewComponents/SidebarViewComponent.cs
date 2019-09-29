using Administration.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Administration.ViewComponents
{
    public class SidebarViewComponent : BaseViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var userId = HttpContext.Session?.GetInt32("userId");
            var username = HttpContext.Session?.GetString("username");
            var header = new SidebarViewModel(userId, username)
            {
                AccessBlog = CheckPermission(permissionName: "AccessBlog"),
                AccessMerchants = CheckPermission(permissionName: "AccessMerchants"),
                AccessOrders = CheckPermission(permissionName: "AccessOrders"),
                AccessItems = CheckPermission(permissionName: "AccessItems"),
                AccessCustomers = CheckPermission(permissionName: "AccessCustomers"),
                AccessRoles = CheckPermission(permissionName: "AccessRoles"),
                AccessPermissions = CheckPermission(permissionName: "AccessPermissions"),
                AccessRolePermissions = CheckPermission(permissionName: "AccessRolePermissions"),
                AccessThemes = CheckPermission(permissionName: "AccessThemes"),
                AccessBlogCategoryTypes = CheckPermission(permissionName: "AccessBlogCategoryTypes"),
                AccessBlogStatusTypes = CheckPermission(permissionName: "AccessBlogStatusTypes"),
                AccessItemTypes = CheckPermission(permissionName: "AccessItemTypes"),
                AccessMerchantTypes = CheckPermission(permissionName: "AccessMerchantTypes"),
                AccessOrderStatusTypes = CheckPermission(permissionName: "AccessOrderStatusTypes"),
                AccessPaymentStatusTypes = CheckPermission(permissionName: "AccessPaymentStatusTypes"),
                AccessPaymentTypes = CheckPermission(permissionName: "AccessPaymentTypes"),
                AccessPriceTypes = CheckPermission(permissionName: "AccessPriceTypes"),
                AccessUnitTypes = CheckPermission(permissionName: "AccessUnitTypes")
            };
            return View("Sidebar", header);
        }
    }
}
