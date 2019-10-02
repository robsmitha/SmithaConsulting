using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Administration.Models
{
    public class FooterViewModel : LayoutViewModel
    {
        public FooterViewModel() { }
        public FooterViewModel(int? userId, string username)
        {
            UserId = userId;
            IsLoggedIn = userId > 0;
        }
    }
}
