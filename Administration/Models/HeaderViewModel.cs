using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Administration.Models
{
    public class HeaderViewModel : LayoutViewModel
    {
        public HeaderViewModel() { }
        public HeaderViewModel(int? userId, int? merchantId, string username)
        {
            UserId = userId;
            IsLoggedIn = userId > 0;
            MerchantId = merchantId;
            Greeting = username;
        }
    }
}
