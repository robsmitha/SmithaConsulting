using Domain.Entities;
using System;

namespace Domain.Models
{
    public class LineItemModel
    {
        public int ID { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public decimal ItemAmount { get; set; }
        public int ItemID { get; set; }
        public int OrderID { get; set; }
        public string ItemName { get; set; }
        public int ItemTypeID { get; set; }
        public string ItemDescription { get; set; }
        public DateTime OrderCreatedAt { get; set; }
        public int OrderOrderStatusTypeID { get; set; }
        public int OrderMerchantID { get; set; }
        public string OrderMerchantMerchantName { get; set; }
        public LineItemModel() { }
    }
}
