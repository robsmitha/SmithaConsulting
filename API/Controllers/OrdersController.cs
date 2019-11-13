using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DataLayer;
using DataLayer.DAL;
using DataLayer.Data;
using DomainLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public OrdersController(DbArchitecture context, IMapper mapper)
        {
            _unitOfWork = new UnitOfWork(context);
            _mapper = mapper;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<OrderModel>> Get()
        {
            var orders = _unitOfWork.OrderRepository.GetAll(includeProperties: "Customer,Merchant,OrderStatusType,User");         
            return Ok(_mapper.Map<IEnumerable<OrderModel>>(orders));
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<OrderModel> Get(int id)
        {
            var order = _unitOfWork.OrderRepository.Get(x => x.ID == id, includeProperties: "Customer,Merchant,OrderStatusType,User");
            if(order == null)
            {
                return NotFound();
            }
            //var lineItems = _unitOfWork.OrderRepository.GetLineItems(order);
            //var payments = _unitOfWork.OrderRepository.GetPayments(order);
            var model = _mapper.Map<OrderModel>(order);
            //model.LineItems = _mapper.Map<List<LineItemModel>>(lineItems);
            //model.Payments = _mapper.Map<List<PaymentModel>>(payments);
            return Ok(model);
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult<OrderModel>> Post(OrderModel model)
        {
            var order = _mapper.Map<Order>(model);
            _unitOfWork.OrderRepository.Add(order);
            await Task.Run(() => _unitOfWork.Save());
            return CreatedAtAction("Get", new { id = order.ID });
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<ActionResult<OrderModel>> Put(int id, OrderModel model)
        {
            var order = _unitOfWork.OrderRepository
                .Get(x => x.ID == id, 
                includeProperties: "Customer,Merchant,OrderStatusType,User");          
            if(order != null)
            {
                try
                {
                    _mapper.Map(model, order);
                    _unitOfWork.OrderRepository.Update(order);
                    await Task.Run(() => _unitOfWork.Save());
                    return Ok(_mapper.Map<OrderModel>(order));
                }
                catch(Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }
            }
            return NotFound();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _unitOfWork.OrderRepository.Delete(id);
            _unitOfWork.Save();
            return Ok();
        }

        [HttpDelete("{id}/lineItems/{itemId}")]
        public ActionResult DeleteLineItemsByItemId(int id, int itemId)
        {
            var entities = _unitOfWork.LineItemRepository.GetAll(filter: x => x.OrderID == id && x.ItemID == itemId);
            _unitOfWork.LineItemRepository.DeleteRange(entities);
            _unitOfWork.Save();
            return Ok();
        }
    }
}
