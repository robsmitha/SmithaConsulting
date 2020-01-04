namespace Store.Models
{
    public class StoreCartViewModel
    {
        public OrderViewModel Order { get; set; }
        public StoreCartViewModel() { }
        public StoreCartViewModel(OrderViewModel order)
        {
            Order = order;
        }
    }
}
