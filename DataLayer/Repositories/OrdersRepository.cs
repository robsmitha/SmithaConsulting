using DataLayer.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public class OrdersRepository : GenericRepository<Order>
    {
        public partial class OrderRow
        {
            public Order order { get; set; }
            public LineItem lineItem { get; set; }
            public Item item { get; set; }
            public Payment payment { get; set; }
            public Customer customer { get; set; }
            public Merchant merchant { get; set; }
            public OrderStatusType orderStatusType { get; set; }
            public User user { get; set; }
        }

        public OrdersRepository(DbArchitecture context) : base(context) { }
        //public async Task<List<Order>> GetOrdersAsync(string includeProperties)
        //{
        //    var orders = await GetAllAsync(includeProperties: includeProperties);
        //    return orders.ToList();
        //}
        //public async Task<Order> GetOrderAsync(int id)
        //{
        //    var order = await GetAsync(filter: o => o.ID == id, includeProperties: "Customer,Merchant,OrderStatusType,User"); ;
        //    return order;
        //}

        public IQueryable<OrderRow> GetOrderRows(int id)
        {
            var orderRows = from o in context.Orders
                            join c in context.Customers on o.CustomerID equals c.ID into cResult
                            from subC in cResult.DefaultIfEmpty()
                            join u in context.Users on o.UserID equals u.ID into uResult
                            from subU in uResult.DefaultIfEmpty()
                            join m in context.Merchants on o.MerchantID equals m.ID
                            join ost in context.OrderStatusTypes on o.OrderStatusTypeID equals ost.ID
                            join li in context.LineItems on o.ID equals li.OrderID into liResult
                            from subLi in liResult.DefaultIfEmpty()
                            join i in context.Items on subLi.ItemID equals i.ID into iResult
                            from subI in iResult.DefaultIfEmpty()
                            join p in context.Payments on o.ID equals p.OrderID into pResult
                            from subP in pResult.DefaultIfEmpty()
                            where o.ID == id
                            select new OrderRow
                            {
                                order = o,
                                lineItem = subLi,
                                payment = subP,
                                item = subI,
                                customer = subC,
                                merchant = m,
                                orderStatusType = ost,
                                user = subU
                            };

            return orderRows;
        }
        public IQueryable<OrderRow> GetCustomerOrderRows(int customerId)
        {
            var orderRows = from o in context.Orders
                            join c in context.Customers on o.CustomerID equals c.ID into cResult
                            from subC in cResult.DefaultIfEmpty()
                            join u in context.Users on o.UserID equals u.ID into uResult
                            from subU in uResult.DefaultIfEmpty()
                            join m in context.Merchants on o.MerchantID equals m.ID
                            join ost in context.OrderStatusTypes on o.OrderStatusTypeID equals ost.ID
                            join li in context.LineItems on o.ID equals li.OrderID into liResult
                            from subLi in liResult.DefaultIfEmpty()
                            join i in context.Items on subLi.ItemID equals i.ID into iResult
                            from subI in iResult.DefaultIfEmpty()
                            join p in context.Payments on o.ID equals p.OrderID into pResult
                            from subP in pResult.DefaultIfEmpty()
                            where o.CustomerID == customerId
                            select new OrderRow
                            {
                                order = o,
                                lineItem = subLi,
                                payment = subP,
                                item = subI,
                                customer = subC,
                                merchant = m,
                                orderStatusType = ost,
                                user = subU
                            };

            return orderRows;
        }

        public List<LineItem> GetLineItems(int orderId) => context.LineItems
            .Where(x => x.OrderID == orderId)
            .Include(l => l.Item)
            .ToList();
        public List<Payment> GetPayments(int orderId) => context.Payments
            .Where(x => x.OrderID == orderId)
            .ToList();

    }
}
