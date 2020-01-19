using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Models
{
    public class ItemViewModel
    {
        public int ID { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public int ItemTypeID { get; set; }
        public int MerchantID { get; set; }
        public decimal? Price { get; set; }
        public int PriceTypeID { get; set; }
        public int UnitTypeID { get; set; }
        public string Code { get; set; }
        public string LookupCode { get; set; }
        public decimal? Percentage { get; set; }
    }
}
