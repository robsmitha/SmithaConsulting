using System;
using System.Collections.Generic;
using System.Text;

namespace Architecture.Models
{
    public class PaymentModel
    {
        public int ID { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public decimal Amount { get; set; }
        public decimal? CashTendered { get; set; }
        public int PaymentTypeID { get; set; }
        public int PaymentStatusTypeID { get; set; }
        public int? AuthorizationID { get; set; }
        public int OrderID { get; set; }
        public string PaymentTypeDescription { get; set; }
    }
}
