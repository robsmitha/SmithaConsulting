using Architecture.DTOs;

namespace Store.Models
{
    public class RegisterViewModel
    {
        public int CustomerID { get; set; }
        public int UserID { get; set; }
        public int SelectedItemID { get; set; }
        public int SelectedLineItemID { get; set; }
        public int? CurrentOrderID { get; set; }
        public OrderDTO Order { get; set; }
        public RegisterViewModel() { }
        public RegisterViewModel(OrderDTO order)
        {
            Order = order;
            CurrentOrderID = order?.ID;
        }
    }
}
