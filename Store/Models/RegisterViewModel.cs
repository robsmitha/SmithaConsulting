using DataLayer.Models;

namespace Store.Models
{
    public class RegisterViewModel
    {
        public int CustomerID { get; set; }
        public int UserID { get; set; }
        public int SelectedItemID { get; set; }
        public int SelectedLineItemID { get; set; }
        public int? CurrentOrderID { get; set; }
        public OrderModel Order { get; set; }
        public RegisterViewModel() { }
        public RegisterViewModel(OrderModel order)
        {
            Order = order;
            CurrentOrderID = order?.ID;
        }
    }
}
