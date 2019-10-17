using System.Collections.Generic;
using System.Linq;
using DataModeling;
using DataModeling.Data;
using Architecture.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : Controller
    {
        private readonly DbArchitecture _context;
        public ItemsController(DbArchitecture context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ItemDTO>> Get()
        {
            var items = _context.Items
                .Include(o => o.Merchant);

            return items.Select(x => new ItemDTO(x)).ToArray();
        }

        [HttpGet("{id}")]
        public ActionResult<ItemDTO> Get(int id)
        {
            var item = _context.Items
                   .Include(o => o.Merchant)
                   .SingleOrDefault(x => x.ID == id);
            var dto = new ItemDTO(item);
            return dto;
        }

        [HttpPost]
        public int Post([FromBody] ItemDTO dto)
        {
            var item = new Item
            {
                MerchantID = dto.MerchantID,
                CreatedAt = dto.CreatedAt,
                Active = true
            };
            _context.Add(item);
            _context.SaveChanges();

            return item.ID;
        }

        [HttpPut("{id}")]
        public int Put(int id, [FromBody] ItemDTO dto)
        {
            var item = _context.Items.SingleOrDefault(x => x.ID == id);
            if (item == null)
            {
                return 0;
            }

            _context.Update(item);
            _context.SaveChanges();

            return item.ID;
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var item = _context.Items.SingleOrDefault(x => x.ID == id);
            if (item == null)
            {
                return;
            }
            _context.Remove(item);
            _context.SaveChanges();
        }
    }
}