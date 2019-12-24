using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataLayer;
using DataLayer.Data;
using DomainLayer.Models;
using DataLayer.DAL;
using AutoMapper;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CustomersController(DbArchitecture context, IMapper mapper)
        {
            _unitOfWork = new UnitOfWork(context);
            _mapper = mapper;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerModel>>> GetCustomers()
        {
            var customers = await _unitOfWork.CustomerRepository.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<CustomerModel>>(customers));
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerModel>> GetCustomer(int id)
        {
            var customer = await _unitOfWork.CustomerRepository.GetAsync(c => c.ID == id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CustomerModel>(customer));
        }

        // PUT: api/Customers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, CustomerModel model)
        {
            if (id != model.ID)
            {
                return BadRequest();
            }

            var customer = _unitOfWork.CustomerRepository.Get(x => x.ID == id);

            if (customer == null)
            {
                return NotFound();
            }

            _mapper.Map(model, customer);

            _unitOfWork.CustomerRepository.Update(customer);

            try
            {
                await _unitOfWork.SaveAsync();
                return Ok();
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
                var customer = _mapper.Map<Customer>(model);
                _unitOfWork.CustomerRepository.Add(customer);
                await _unitOfWork.SaveAsync();
                return _mapper.Map<CustomerModel>(customer);
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
            _unitOfWork.CustomerRepository.Delete(id);
            await _unitOfWork.SaveAsync();
            return Ok();
        }
    }
}
