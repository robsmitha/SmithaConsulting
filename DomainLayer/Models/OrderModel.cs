using System;
using System.Collections.Generic;
using DomainLayer.Entities;

namespace DomainLayer.Models
{
    public class OrderModel
    {
        public int ID { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string Note { get; set; }
        public decimal Total { get; set; }
        public int OrderStatusTypeID { get; set; }
        public int MerchantID { get; set; }
        public int? CustomerID { get; set; }
        public int? UserID { get; set; }
        public string OrderStatusTypeName { get; set; }
        public List<LineItemModel> LineItems { get; set; }
        public List<PaymentModel> Payments { get; set; }

        public OrderModel() { }

    }
}
