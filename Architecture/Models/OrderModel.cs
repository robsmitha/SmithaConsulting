using System;
using System.Collections.Generic;
using DataModeling;

namespace Architecture.Models
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
        public string OrderStatusType { get; set; }
        public List<LineItemModel> LineItems { get; set; }
        public List<PaymentModel> Payments { get; set; }

        public OrderModel(Order order)
        {
            if (order != null)
            {
                ID = order.ID;
                CreatedAt = order.CreatedAt;
                Note = order.Note;
                Total = order.Total;
                OrderStatusTypeID = order.OrderStatusTypeID;
                MerchantID = order.MerchantID;
                CustomerID = order.CustomerID;
                UserID = order.UserID;
                OrderStatusType = order.OrderStatusType?.Description;
            }
        }

        public OrderModel() { }

    }
}
