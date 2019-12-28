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
using DataLayer.Repositories;
using AutoMapper;
using DomainLayer.BLL;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MerchantsController : ControllerBase
    {
        private readonly BusinessLogic BLL;
        public MerchantsController(DbArchitecture context, IMapper mapper)
        {
            if (BLL == null)
            {
                BLL = new BusinessLogic(context, mapper);
            }
        }

        // GET: api/Merchants
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MerchantModel>>> GetMerchants()
        {
            try
            {
                var merchants = await BLL.Merchants.GetAllAsync();
                return Ok(merchants);
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
            try
            {
                var items = await BLL.Merchants.GetMerchantItems(id);
                return Ok(items);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        // GET: api/Merchants/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MerchantModel>> GetMerchant(int id)
        {
            try
            {
                var merchant = await BLL.Merchants.GetAsync(id);
                if (merchant == null)
                {
                    return NotFound();
                }
                return Ok(merchant);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        // PUT: api/Merchants/5
        [HttpPut("{id}")]
        public async Task<ActionResult<MerchantModel>> PutMerchant(int id, MerchantModel model)
        {
            if (id != model.ID)
            {
                return BadRequest();
            }
            try
            {
                var merchant = await BLL.Merchants.UpdateAsync(model);
                if (merchant == null)
                {
                    return NotFound();
                }
                return Ok(merchant);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        // POST: api/Merchants
        [HttpPost]
        public async Task<ActionResult<MerchantModel>> PostMerchant(MerchantModel model)
        {
            try
            {
                return Ok(await BLL.Merchants.AddAsync(model));
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
                await BLL.Applications.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

    }
}
