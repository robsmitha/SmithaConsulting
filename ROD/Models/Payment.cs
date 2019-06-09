using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ROD.Models
{
    public class Payment : BaseModel
    {
        public decimal Amount { get; set; }
        public int PaymentTypeID { get; set; }

        [ForeignKey("PaymentTypeID")]
        public PaymentType PaymentType { get; set; }

        [ForeignKey("OrderID")]
        public Order Order { get; set; }
    }
}
