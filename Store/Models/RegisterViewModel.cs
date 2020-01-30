using Domain.Models;

namespace Store.Models
{
    public class RegisterViewModel : LayoutViewModel
    {
        public int SelectedItemID { get; set; }
        public int SelectedLineItemID { get; set; }
        public OrderModel Order { get; set; }
        public RegisterViewModel() { }
        public RegisterViewModel(OrderModel order, int? userId) : base(userId)
        {
            Order = order;
        }
    }
}
