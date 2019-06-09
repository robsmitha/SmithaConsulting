using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ROD.Models
{
    public class Order : BaseModel
    {
        public string Note { get; set; }
        public decimal Total { get; set; }
        public int OrderStatusTypeID { get; set; }
        public int MerchantID { get; set; }
        public int CustomerID { get; set; }

        [ForeignKey("CustomerID")]
        public Customer Customer { get; set; }

        [ForeignKey("MerchantID")]
        public Merchant Merchant { get; set; }

        [ForeignKey("OrderStatusTypeID")]
        public OrderStatusType OrderStatusType { get; set; }
    }
}
