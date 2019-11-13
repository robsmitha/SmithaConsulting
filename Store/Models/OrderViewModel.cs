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
        public OrderViewModel(OrderModel order, List<LineItemModel> lineItems, List<PaymentModel> payments)
        {
            Order = order;
            LineItems = lineItems;
            Payments = payments;
        }
    }
}
