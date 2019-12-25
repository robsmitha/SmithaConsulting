
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataLayer;
using DataLayer.Data;
using DataLayer.DAL;
using AutoMapper;
using DomainLayer.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PaymentsController(DbArchitecture context, IMapper mapper)
        {
            _unitOfWork = new UnitOfWork(context);
            _mapper = mapper;
        }

        // GET: api/Payments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentModel>>> GetPayments()
        {
            try
            {
                var payments = await _unitOfWork
                .PaymentRepository
                .GetAllAsync();
                return Ok(_mapper.Map<IEnumerable<PaymentModel>>(payments));
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
            var payment = await _unitOfWork
                .PaymentRepository
                .GetAsync(p => p.ID == id);

            if (payment == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<PaymentModel>(payment));
        }

        // PUT: api/Payments/5
        [HttpPut("{id}")]
        public async Task<ActionResult<PaymentModel>> PutPayment(int id, PaymentModel model)
        {
            if (id != model.ID)
            {
                return BadRequest();
            }

            var payment = await _unitOfWork.PaymentRepository.GetAsync(p => p.ID == id);
            
            if (payment == null)
            {
                return NotFound();
            }

            try
            {
                _mapper.Map(model, payment);
                _unitOfWork.PaymentRepository.Update(payment);
                await _unitOfWork.SaveAsync();
                return Ok(_mapper.Map<PaymentModel>(payment));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // POST: api/Payments
        [HttpPost]
        public async Task<ActionResult<PaymentModel>> PostPayment(PaymentModel model)
        {
            try
            {
                var payment = _mapper.Map<Payment>(model);
                _unitOfWork.PaymentRepository.Add(payment);
                await _unitOfWork.SaveAsync();
                return Ok(_mapper.Map<PaymentModel>(payment));
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
                _unitOfWork.PaymentRepository.Delete(id);
                await _unitOfWork.SaveAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

    }
}
