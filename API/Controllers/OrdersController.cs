using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Architecture;
using Architecture.Data;
using Architecture.DTOs;
using Architecture.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly DbArchitecture _context;
        public OrdersController(DbArchitecture context)
        {
            _context = context;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<OrderDTO>> Get()
        {
            var orders = _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Merchant)
                .Include(o => o.OrderStatusType)
                .Include(o => o.User);

            return orders.Select(x => new OrderDTO(x)).ToArray();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<OrderDTO> Get(int id)
        {
           var order = _context.Orders
                  .Include(o => o.Customer)
                  .Include(o => o.Merchant)
                  .Include(o => o.OrderStatusType)
                  .Include(o => o.User)
                  .SingleOrDefault(x => x.ID == id);
            var orderDTO = new OrderDTO(order);
            return orderDTO;
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult<OrderDTO>> Post( OrderDTO dto)
        {
            var order = new Order
            {
                OrderStatusTypeID = (int)OrderStatusTypeEnums.Open,
                MerchantID = dto.MerchantID,
                UserID = dto.UserID,
                CustomerID = dto.CustomerID,
                CreatedAt = dto.CreatedAt,
                Active = true
            };
            _context.Add(order);
            await _context.SaveChangesAsync();

            return new OrderDTO(order);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<ActionResult<OrderDTO>> Put(int id, OrderDTO dto)
        {
            var order = _context.Orders.SingleOrDefault(x => x.ID == id);
            if(order != null)
            {
                order.OrderStatusTypeID = dto.OrderStatusTypeID;
                _context.Update(order);
                await _context.SaveChangesAsync();
                return new OrderDTO(order);

            }
            return NotFound();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var order = _context.Orders.SingleOrDefault(x => x.ID == id);
            if (order == null)
            {
                return;
            }
            _context.Remove(order);
            _context.SaveChanges();
        }
    }
}
