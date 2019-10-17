using DataModeling;

namespace Architecture.DTOs
{
    public class MerchantDTO
    {
        public int ID { get; set; }
        public string MerchantName { get; set; }
        public int MerchantTypeID { get; set; }
        public bool Active { get; set; }
        public MerchantDTO(Merchant merchant)
        {
            if (merchant != null)
            {
                ID = merchant.ID;
                MerchantName = merchant.MerchantName;
                MerchantTypeID = merchant.MerchantTypeID;
                Active = merchant.Active;
            }
        }

        public MerchantDTO() { }
    }
}
