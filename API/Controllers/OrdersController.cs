using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataModeling;
using Architecture.DAL;
using DataModeling.Data;
using Architecture.DTOs;
using Architecture.Enums;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly UnitOfWork unitOfWork;
        public OrdersController(DbArchitecture context)
        {
            unitOfWork = new UnitOfWork(context);
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<OrderDTO>> Get()
        {
            var orders = unitOfWork.OrderRepository.Get(includeProperties: "Customer,Merchant,OrderStatusType,User");
            return orders.Select(x => new OrderDTO(x)).ToArray();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<OrderDTO> Get(int id)
        {
            var order = unitOfWork.OrderRepository.Get(x => x.ID == id, includeProperties: "Customer,Merchant,OrderStatusType,User").FirstOrDefault();
            if(order == null)
            {
                return NotFound();
            }
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

            unitOfWork.OrderRepository.Insert(order);
            await System.Threading.Tasks.Task.Run(() => unitOfWork.Save());

            return new OrderDTO(order);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<ActionResult<OrderDTO>> Put(int id, OrderDTO dto)
        {
            var order = unitOfWork.OrderRepository
                .Get(x => x.ID == id, 
                includeProperties: "Customer,Merchant,OrderStatusType,User")
                .FirstOrDefault();          
            if(order != null)
            {
                order.OrderStatusTypeID = dto.OrderStatusTypeID;
                unitOfWork.OrderRepository.Update(order);
                await System.Threading.Tasks.Task.Run(() => unitOfWork.Save());
                return Ok(new OrderDTO(order));
            }
            return NotFound();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            unitOfWork.OrderRepository.Delete(id);
            unitOfWork.Save();
            return Ok();
        }

        [HttpDelete("{id}/lineItems/{itemId}")]
        public ActionResult DeleteLineItemsByItemId(int id, int itemId)
        {
            var entities = unitOfWork.LineItemRepository.Get(filter: x => x.OrderID == id && x.ItemID == itemId);
            unitOfWork.LineItemRepository.DeleteRange(entities);
            unitOfWork.Save();
            return Ok();
        }
    }
}
