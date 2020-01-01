
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataLayer.Data;
using AutoMapper;
using DomainLayer.Models;
using API.BLL;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly BusinessLogic BLL;
        public PaymentsController(OperationsContext context, IMapper mapper)
        {
            if (BLL == null)
            {
                BLL = new BusinessLogic(context, mapper);
            }
        }

        // GET: api/Payments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentModel>>> GetPayments()
        {
            try
            {
                var payments = await BLL.Payments.GetAllAsync();
                return Ok(payments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        // GET: api/Payments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentModel>> GetPayment(int id)
        {
            try
            {
                var payment = await BLL.Payments.GetAsync(id);
                if (payment == null)
                {
                    return NotFound();
                }
                return Ok(payment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        // PUT: api/Payments/5
        [HttpPut("{id}")]
        public async Task<ActionResult<PaymentModel>> PutPayment(int id, PaymentModel model)
        {
            if (id != model.ID)
            {
                return BadRequest();
            }
            try
            {
                var payment = await BLL.Payments.UpdateAsync(model);
                if (payment == null)
                {
                    return NotFound();
                }
                return Ok(payment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        // POST: api/Payments
        [HttpPost]
        public async Task<ActionResult<PaymentModel>> PostPayment(PaymentModel model)
        {
            try
            {
                return Ok(await BLL.Payments.AddAsync(model));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        // DELETE: api/Payments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            try
            {
                await BLL.Payments.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

    }
}
