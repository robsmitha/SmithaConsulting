using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataLayer;
using DataLayer.DAL;
using DataLayer.Data;
using DomainLayer.BLL;
using DomainLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly BusinessLogic BLL;
        public OrdersController(DbArchitecture context, IMapper mapper)
        {
            if (BLL == null)
            {
                BLL = new BusinessLogic(context, mapper);
            }
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderModel>>> Get()
        {
            try
            {
                //TODO: pass properties in from query string
                var includedProperties = "Customer,Merchant,OrderStatusType,User";
                var orders = await BLL.Orders.GetOrderModelsAsync(includedProperties);
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderModel>> Get(int id)
        {
            try
            {
                var includedProperties = "Customer,Merchant,OrderStatusType,User";
                var order = await BLL.Orders.GetOrderModelAsync(id, includedProperties);
                
                if(order == null)
                {
                    return NotFound();
                }

                var lineItems = await BLL.Orders.GetLineItemModelsAsync(order.ID);
                var payment = await BLL.Orders.GetPaymentsAsync(order.ID);
                order.LineItems = lineItems.ToList();
                order.Payments = payment.ToList();
                return Ok(order);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        // POST api/values
        [HttpPost]
        public async Task<ActionResult<OrderModel>> Post(OrderModel model)
        {
            try
            {
                var order = await BLL.Orders.AddOrderAsync(model);
                return Ok(order);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<ActionResult<OrderModel>> Put(int id, OrderModel model)
        {
            if (id != model.ID)
            {
                return BadRequest();
            }

            try
            {

                var order = await BLL.Orders.UpdateOrderAsync(model);
                if (order == null)
                {
                    return NotFound();
                }
                return Ok(order);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await Task.Run(() => BLL.Orders.DeleteOrder(id));
                return Ok();
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpDelete("{id}/lineItems/{itemId}")]
        public async Task<IActionResult> DeleteLineItemsByItemId(int id, int itemId)
        {
            try
            {
                await Task.Run(() => BLL.Orders.DeleteLineItemsByItemId(id, itemId));
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost("{id}/lineItems")]
        public async Task<ActionResult> BulkCreateLineItems(List<LineItemModel> lineItems)
        {
            try
            {
                if(lineItems?.Count > 0)
                {
                    return Ok();
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
