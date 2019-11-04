using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataModeling;
using DataModeling.Data;
using Architecture.DTOs;
using Architecture.DAL;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LineItemsController : ControllerBase
    {
        private readonly UnitOfWork unitOfWork;
        public LineItemsController(DbArchitecture context)
        {
            unitOfWork = new UnitOfWork(context);
        }

        [HttpGet]
        public ActionResult<IEnumerable<LineItemDTO>> Get()
        {
            var lineItems = unitOfWork.LineItemRepository.Get(includeProperties: "Item");
            try
            {
                return Ok(lineItems.Select(x => new LineItemDTO(x)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<LineItemDTO>> Get(int id)
        {
            var data = await unitOfWork.LineItemRepository
                .Get(x => x.ID == id, includeProperties: "Item")
                .AsQueryable()
                .FirstOrDefaultAsync();

            if (data == null)
            {
                return NotFound();
            }

            var dto = new LineItemDTO(data);
            return dto;
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<LineItemDTO>> Put(int id, LineItemDTO dto)
        {
            var data = unitOfWork.LineItemRepository.Get(x => x.ID == id, includeProperties: "Customer,Merchant,OrderStatusType,User").FirstOrDefault();

            if (data != null)
            {
                unitOfWork.LineItemRepository.Update(data);
                await System.Threading.Tasks.Task.Run(() => unitOfWork.Save());
                return new LineItemDTO(data);
            }

            return NotFound();
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

            unitOfWork.LineItemRepository.Insert(lineItem);
            await System.Threading.Tasks.Task.Run(() => unitOfWork.Save());

            return new LineItemDTO(lineItem);
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            unitOfWork.OrderRepository.Delete(id);

        }
    }
}
