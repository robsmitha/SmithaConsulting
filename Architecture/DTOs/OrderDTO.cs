using System;
using DataModeling;

namespace Architecture.DTOs
{
    public class OrderDTO
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

        public OrderDTO(Order order)
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

        public OrderDTO() { }

    }
}
