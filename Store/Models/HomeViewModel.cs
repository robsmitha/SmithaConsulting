using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Models
{
    public class HomeViewModel : LayoutViewModel
    {
        public HomeViewModel(int? userId) : base(userId)
        {
            UserID = userId;
            IsLoggedIn = userId > 0;
        }
    }
}
