using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class Refund : BaseModel
    {
        public decimal Amount { get; set; }
        public int VoidReasonTypeID { get; set; }
        public int OrderID { get; set; }
        public int? PaymentID { get; set; }

        [ForeignKey("VoidReasonTypeID")]
        public VoidReasonType VoidReasonType { get; set; }

        [ForeignKey("OrderID")]
        public Order Order { get; set; }

        [ForeignKey("PaymentID")]
        public Payment Payment { get; set; }
    }
}
