using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rodcon.Models
{
    public class PaymentViewModel
    {
        public string PromoCode { get; set; }
        public int CurrentOrderID { get; set; }
        public OrderViewModel OrderViewModel { get; set; }
        public PaymentViewModel() { }
        public PaymentViewModel(OrderViewModel orderViewModel, int orderId)
        {
            OrderViewModel = orderViewModel;
            CurrentOrderID = orderId;
        }
    }
}
