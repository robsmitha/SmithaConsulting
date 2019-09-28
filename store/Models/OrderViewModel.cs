using Architecture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Models
{
    public class OrderViewModel
    {
        public Order Order { get; set; }
        public List<LineItem> LineItems { get; set; }
        public List<Payment> Payments { get; set; }
        public OrderViewModel() { }
        public OrderViewModel(Order order, List<LineItem> lineItems, List<Payment> payments)
        {
            Order = order;
            LineItems = lineItems;
            Payments = payments;
        }
    }
}
