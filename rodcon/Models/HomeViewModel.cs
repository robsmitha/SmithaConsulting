using rod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rodcon.Models
{
    public class HomeViewModel
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
        //public IEnumerable<Permission> UserPermissions { get; set; }
        //public HomeViewModel(IEnumerable<Permission> userPermissions)
        //{
        //    UserPermissions = userPermissions;
        //}
    }
}
