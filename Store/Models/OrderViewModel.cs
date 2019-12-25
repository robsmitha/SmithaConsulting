using DomainLayer.Models;
using System.Collections.Generic;

namespace Store.Models
{
    public class OrderViewModel
    {
        public OrderModel Order { get; set; }
        public List<LineItemModel> LineItems { get; set; }
        public List<PaymentModel> Payments { get; set; }
        public OrderViewModel() { }
        public OrderViewModel(OrderModel order)
        {
            Order = order;
            LineItems = order?.LineItems ?? new List<LineItemModel>();
            Payments = order?.Payments ?? new List<PaymentModel>();
        }
    }
}
