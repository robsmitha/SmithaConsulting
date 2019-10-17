using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataModeling;
using DataModeling.Data;
using Architecture.DTOs;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LineItemsController : ControllerBase
    {
        private readonly DbArchitecture _context;

        public LineItemsController(DbArchitecture context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<LineItemDTO>>> Get()
        {
            var lineItems = await _context.LineItems.Include(o => o.Item).Select(x => new LineItemDTO(x)).ToListAsync();
            return lineItems;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<LineItemDTO>> Get(int id)
        {
            var lineItem = await _context.LineItems.Include(o => o.Item).FirstOrDefaultAsync(x => x.ID == id);

            if (lineItem == null)
            {
                return NotFound();
            }
            var dto = new LineItemDTO(lineItem);
            return dto;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, LineItemDTO lineItem)
        {
            if (id != lineItem.ID)
            {
                return BadRequest();
            }

            _context.Entry(lineItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LineItemExists(id))
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


        [HttpPost]
        public async Task<ActionResult<LineItemDTO>> Post(LineItemDTO dto)
        {
            var lineItem = new LineItem
            {
                CreatedAt = DateTime.Now,
                ItemAmount = dto.ItemAmount,
                ItemID = dto.ItemID,
                OrderID = dto.OrderID,
                Active = true
            };
            _context.Add(lineItem);
            await _context.SaveChangesAsync();

            return new LineItemDTO(lineItem);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<LineItemDTO>> Delete(int id)
        {
            var lineItem = await _context.LineItems.FindAsync(id);
            if (lineItem == null)
            {
                return NotFound();
            }

            _context.LineItems.Remove(lineItem);
            await _context.SaveChangesAsync();
            
            var dto = new LineItemDTO(lineItem);
            return dto;
        }

        private bool LineItemExists(int id)
        {
            return _context.LineItems.Any(e => e.ID == id);
        }
    }
}
