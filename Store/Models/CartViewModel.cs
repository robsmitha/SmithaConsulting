namespace Store.Models
{
    public class CartViewModel
    {
        public OrderViewModel Order { get; set; }
        public CartViewModel() { }
        public CartViewModel(OrderViewModel order)
        {
            Order = order;
        }
    }
}
