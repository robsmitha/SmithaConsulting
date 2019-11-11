namespace Store.Models
{
    public class RegisterCartViewModel
    {
        public OrderViewModel Order { get; set; }
        public RegisterCartViewModel() { }
        public RegisterCartViewModel(OrderViewModel order)
        {
            Order = order;
        }
    }
}
