using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Administration.Models
{
    public class SidebarViewModel : LayoutViewModel
    {
        public bool AccessBlog { get; set; }
        public bool AccessMerchants { get; set; }
        public bool AccessOrders { get; set; }
        public bool AccessItems { get; set; }
        public bool AccessCustomers { get; set; }
        public bool AccessRoles { get; set; }
        public bool AccessPermissions { get; set; }
        public bool AccessRolePermissions { get; set; }
        public bool AccessThemes { get; set; }
        public bool AccessBlogCategoryTypes { get; set; }
        public bool AccessBlogStatusTypes { get; set; }
        public bool AccessItemTypes { get; set; }
        public bool AccessMerchantTypes { get; set; }
        public bool AccessOrderStatusTypes { get; set; }
        public bool AccessPaymentStatusTypes { get; set; }
        public bool AccessPaymentTypes { get; set; }
        public bool AccessPriceTypes { get; set; }
        public bool AccessUnitTypes { get; set; }

        public SidebarViewModel() { }
        public SidebarViewModel(int? userId, string username)
        {
            UserId = userId;
            IsLoggedIn = userId > 0;
            Greeting = username;
        }
    }
}
