using Architecture;
using System.Collections.Generic;

namespace Store.Models
{
    public class RegisterViewModel
    {
        public int CustomerID { get; set; }
        public int UserID { get; set; }
        public int SelectedItemID { get; set; }
        public int SelectedLineItemID { get; set; }
        public int? CurrentOrderID { get; set; }
        public Order Order { get; set; }
        public RegisterViewModel() { }
        public RegisterViewModel(Order order)
        {
            Order = order;
            CurrentOrderID = order?.ID;
        }
    }
}
