using DataLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer.DAL
{
    public class OrdersRepository : GenericRepository<Order>
    {
        public OrdersRepository(DbArchitecture context) : base(context) { }

        public List<LineItem> GetLineItems(Order order) => context.LineItems
                .Where(x => x.OrderID == order.ID)
                .ToList();
        public List<Payment> GetPayments(Order order) => context.Payments
            .Where(x => x.OrderID == order.ID)
            .ToList();
    }
}
