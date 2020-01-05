using Domain.Entities;

namespace Domain.Models
{
    public class MerchantModel
    {
        public int ID { get; set; }
        public string MerchantName { get; set; }
        public string WebsiteUrl { get; set; }
        public int MerchantTypeID { get; set; }
        public bool Active { get; set; }
        public string MerchantTypeName { get; set; }
    }
}
