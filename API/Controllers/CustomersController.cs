using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataLayer;
using DataLayer.Data;
using DomainLayer.Models;
using DataLayer.Repositories;
using AutoMapper;
using DomainLayer.BLL;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly BusinessLogic BLL;

        public CustomersController(DbArchitecture context, IMapper mapper)
        {
            if (BLL == null)
            {
                BLL = new BusinessLogic(context, mapper);
            }
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerModel>>> GetCustomers()
        {
            try
            {
                var customers = await BLL.Customers.GetAllAsync();
                return Ok(customers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerModel>> GetCustomer(int id)
        {
            try
            {
                var customer = await BLL.Customers.GetAsync(id);
                if (customer == null)
                {
                    return NotFound();
                }

                return Ok(customer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        // GET: api/Customers/5
        [HttpGet("{id}/orders")]
        public async Task<ActionResult<IEnumerable<OrderModel>>> GetCustomerOrders(int id)
        {
            try
            {
                var orders = await Task.Run(() => BLL.Orders.GetCustomerOrderModels(id));
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        // PUT: api/Customers/5
        [HttpPut("{id}")]
        public async Task<ActionResult<CustomerModel>> PutCustomer(int id, CustomerModel model)
        {
            if (id != model.ID)
            {
                return BadRequest();
            }
            try
            {
                var customer = await BLL.Customers.UpdateAsync(model);

                if (customer == null)
                {
                    return NotFound();
                }
                return Ok(customer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        // POST: api/Customers
        [HttpPost]
        public async Task<ActionResult<CustomerModel>> PostCustomer(CustomerModel model)
        {
            try
            {
                return Ok(await BLL.Customers.AddAsync(model));
            }
            catch(Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCustomer(int id)
        {
            try
            {
                await BLL.Customers.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
