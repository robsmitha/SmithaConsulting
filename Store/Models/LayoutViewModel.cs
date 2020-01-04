using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Models
{
    public class LayoutViewModel
    {
        public string Greeting { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public bool IsLoggedIn { get; set; } = false;
        public int? UserID { get; set; } = null;
        public int? MerchantID { get; set; } = null;
        public int CustomerID { get; set; }
        public LayoutViewModel() { }
        public LayoutViewModel(int? userId)
        {
            UserID = userId;
            IsLoggedIn = userId > 0;
        }
        public LayoutViewModel(int? userId, int? merchantId, string username, string imageUrl = null)
        {
            UserID = userId;
            IsLoggedIn = userId > 0;
            MerchantID = merchantId;
            Greeting = $"Hi, {username}";
            ImageUrl = imageUrl;        
        }
    }
}
