using DomainLayer.Models;

namespace Store.Models
{
    public class StoreViewModel : LayoutViewModel
    {
        public int SelectedItemID { get; set; }
        public int SelectedLineItemID { get; set; }
        public int? CurrentOrderID { get; set; }
        public OrderModel Order { get; set; }
        public StoreViewModel() { }
        public StoreViewModel(OrderModel order, int? userId) : base(userId)
        {
            Order = order;
            CurrentOrderID = order?.ID;
        }
    }
}
