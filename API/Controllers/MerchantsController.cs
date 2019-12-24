using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
    public class MerchantsController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MerchantsController(DbArchitecture context, IMapper mapper)
        {
            _unitOfWork = new UnitOfWork(context);
            _mapper = mapper;
        }

        // GET: api/Merchants
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MerchantModel>>> GetMerchants()
        {
            try
            {
                var merchants = await _unitOfWork
                    .MerchantRepository
                    .GetAllAsync(includeProperties: "MerchantType");
                return Ok(_mapper.Map<IEnumerable<MerchantModel>>(merchants));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        // GET: api/Merchants/5/items
        [HttpGet("{id}/items")]
        public async Task<ActionResult<IEnumerable<ItemModel>>> GetMerchantItems(int id)
        {
            var items = await _unitOfWork
                .MerchantRepository
                .GetMerchantItemsAsync(id);
            return Ok(_mapper.Map<IEnumerable<ItemModel>>(items));
        }

        // GET: api/Merchants/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MerchantModel>> GetMerchant(int id)
        {
            var merchant = await _unitOfWork.MerchantRepository.GetAsync(m => m.ID == id);

            if (merchant == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<MerchantModel>(merchant));
        }

        // PUT: api/Merchants/5
        [HttpPut("{id}")]
        public async Task<ActionResult<MerchantModel>> PutMerchant(int id, MerchantModel model)
        {
            if (id != model.ID)
            {
                return BadRequest();
            }

            var merchant = _unitOfWork.MerchantRepository.Get(x => x.ID == id);

            if (merchant == null)
            {
                return NotFound();
            }

            _mapper.Map(model, merchant);

            _unitOfWork.MerchantRepository.Update(merchant);

            try
            {
                await _unitOfWork.SaveAsync();
                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500);
            }
        }

        // POST: api/Merchants
        [HttpPost]
        public async Task<ActionResult<MerchantModel>> PostMerchant(MerchantModel model)
        {
            try
            {
                var merchant = _mapper.Map<Merchant>(model);
                _unitOfWork.MerchantRepository.Add(merchant);
                await _unitOfWork.SaveAsync();
                return CreatedAtAction("GetMerchant", new { id = merchant.ID });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        // DELETE: api/Merchants/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMerchant(int id)
        {
            try
            {
                _unitOfWork.MerchantRepository.Delete(id);
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
