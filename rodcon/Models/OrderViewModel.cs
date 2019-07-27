using rod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rodcon.Models
{
    public class OrderViewModel
    {
        public Order Order { get; set; }
        public List<LineItem> LineItems { get; set; }
        public OrderViewModel() { }
        public OrderViewModel(Order order, List<LineItem> lineItems)
        {
            Order = order;
            LineItems = lineItems;
        }
    }
}
