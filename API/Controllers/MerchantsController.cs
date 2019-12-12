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

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MerchantsController : ControllerBase
    {
        private readonly DbArchitecture _context;

        public MerchantsController(DbArchitecture context)
        {
            _context = context;
        }

        // GET: api/Merchants
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MerchantModel>>> GetMerchants()
        {
            var merchants = _context.Merchants
                .Include(o => o.MerchantType);
            return await merchants.Select(x => new MerchantModel(x)).ToListAsync();
        }

        // GET: api/Merchants/5/items
        [HttpGet("{id}/items")]
        public async Task<ActionResult<IEnumerable<ItemModel>>> GetMerchantItems(int id)
        {
            var items = _context.Items
                .Include(o => o.Merchant)
                .Where(i => i.MerchantID == id);
            return await items.Select(x => new ItemModel(x)).ToListAsync();
        }

        // GET: api/Merchants/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MerchantModel>> GetMerchant(int id)
        {
            var merchant = await _context.Merchants.FindAsync(id);

            if (merchant == null)
            {
                return NotFound();
            }
            var dto = new MerchantModel(merchant);
            return dto;
        }

        // PUT: api/Merchants/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMerchant(int id, Merchant merchant)
        {
            if (id != merchant.ID)
            {
                return BadRequest();
            }

            _context.Entry(merchant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MerchantExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Merchants
        [HttpPost]
        public async Task<ActionResult<Merchant>> PostMerchant(Merchant merchant)
        {
            _context.Merchants.Add(merchant);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMerchant", new { id = merchant.ID }, merchant);
        }

        // DELETE: api/Merchants/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Merchant>> DeleteMerchant(int id)
        {
            var merchant = await _context.Merchants.FindAsync(id);
            if (merchant == null)
            {
                return NotFound();
            }

            _context.Merchants.Remove(merchant);
            await _context.SaveChangesAsync();

            return merchant;
        }

        private bool MerchantExists(int id)
        {
            return _context.Merchants.Any(e => e.ID == id);
        }
    }
}
