using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace rodLib
{
    public class CashEvent : BaseModel
    {
        public decimal AmountChange { get; set; }
        public int CashEventTypeID { get; set; }
        public int? PaymentID { get; set; }
        public int? RefundID { get; set; }

        [ForeignKey("CashEventTypeID")]
        public CashEventType CashEventType { get; set; }

        [ForeignKey("PaymentID")]
        public Payment Payment { get; set; }

        [ForeignKey("RefundID")]
        public Merchant Refund { get; set; }
    }
}
