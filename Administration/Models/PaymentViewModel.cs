using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Administration.Models
{
    public class PaymentViewModel
    {
        public string PromoCode { get; set; }
        public int CurrentOrderID { get; set; }
        public OrderViewModel OrderViewModel { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public PaymentViewModel() { }
        public PaymentViewModel(OrderViewModel orderViewModel, int orderId)
        {
            OrderViewModel = orderViewModel;
            CurrentOrderID = orderId;
        }
    }
}
