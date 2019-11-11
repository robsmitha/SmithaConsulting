using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.DAL;
using DataLayer.Data;
using DataLayer.Models;
using DomainLayer.Enums;
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
        public ActionResult<IEnumerable<OrderModel>> Get()
        {
            var orders = unitOfWork.OrderRepository.GetAll(includeProperties: "Customer,Merchant,OrderStatusType,User");
            return Ok(orders.Select(x => new OrderModel(x)).ToArray());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<OrderModel> Get(int id)
        {
            var order = unitOfWork.OrderRepository.GetAll(x => x.ID == id, includeProperties: "Customer,Merchant,OrderStatusType,User").FirstOrDefault();
            if(order == null)
            {
                return NotFound();
            }
            var orderDTO = new OrderModel(order);
            return Ok(orderDTO);
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult<OrderModel>> Post( OrderModel dto)
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

            unitOfWork.OrderRepository.Add(order);
            await System.Threading.Tasks.Task.Run(() => unitOfWork.Save());

            return Ok(new OrderModel(order));
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<ActionResult<OrderModel>> Put(int id, OrderModel dto)
        {
            var order = unitOfWork.OrderRepository
                .GetAll(x => x.ID == id, 
                includeProperties: "Customer,Merchant,OrderStatusType,User")
                .FirstOrDefault();          
            if(order != null)
            {
                order.OrderStatusTypeID = dto.OrderStatusTypeID;
                unitOfWork.OrderRepository.Update(order);
                await System.Threading.Tasks.Task.Run(() => unitOfWork.Save());
                return Ok(new OrderModel(order));
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
            var entities = unitOfWork.LineItemRepository.GetAll(filter: x => x.OrderID == id && x.ItemID == itemId);
            unitOfWork.LineItemRepository.DeleteRange(entities);
            unitOfWork.Save();
            return Ok();
        }
    }
}
