namespace Store.Models
{
    public class HeaderViewModel : LayoutViewModel
    {
        public HeaderViewModel() { }
        public HeaderViewModel(int? userId, int? merchantId, string username, string imageUrl) : base(userId, merchantId, username, imageUrl)
        {
            
        }
    }
}
