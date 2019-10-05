using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Models
{
    public class PaymentViewModel
    {
        public string PromoCode { get; set; }
        public int CurrentOrderID { get; set; }
        public OrderViewModel OrderViewModel { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
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
