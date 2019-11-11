using DataModeling;

namespace Architecture.Models
{
    public class MerchantModel
    {
        public int ID { get; set; }
        public string MerchantName { get; set; }
        public int MerchantTypeID { get; set; }
        public bool Active { get; set; }
        public MerchantModel(Merchant merchant)
        {
            if (merchant != null)
            {
                ID = merchant.ID;
                MerchantName = merchant.MerchantName;
                MerchantTypeID = merchant.MerchantTypeID;
                Active = merchant.Active;
            }
        }

        public MerchantModel() { }
    }
}
