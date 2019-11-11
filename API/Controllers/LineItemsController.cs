using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataModeling;
using DataModeling.Data;
using Architecture.Models;
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
        public ActionResult<IEnumerable<LineItemModel>> Get()
        {
            var lineItems = unitOfWork.LineItemRepository.GetAll(includeProperties: "Item");
            try
            {
                return Ok(lineItems.Select(x => new LineItemModel(x)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<LineItemModel>> Get(int id)
        {
            var data = await unitOfWork.LineItemRepository
                .GetAll(x => x.ID == id, includeProperties: "Item")
                .AsQueryable()
                .FirstOrDefaultAsync();

            if (data == null)
            {
                return NotFound();
            }

            var dto = new LineItemModel(data);
            return dto;
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<LineItemModel>> Put(int id, LineItemModel dto)
        {
            var data = unitOfWork.LineItemRepository.GetAll(x => x.ID == id, includeProperties: "Customer,Merchant,OrderStatusType,User").FirstOrDefault();

            if (data != null)
            {
                unitOfWork.LineItemRepository.Update(data);
                await System.Threading.Tasks.Task.Run(() => unitOfWork.Save());
                return new LineItemModel(data);
            }

            return NotFound();
        }


        [HttpPost]
        public async Task<ActionResult<LineItemModel>> Post(LineItemModel dto)
        {
            var lineItem = new LineItem
            {
                CreatedAt = DateTime.Now,
                ItemAmount = dto.ItemAmount,
                ItemID = dto.ItemID,
                OrderID = dto.OrderID,
                Active = true
            };

            unitOfWork.LineItemRepository.Add(lineItem);
            await System.Threading.Tasks.Task.Run(() => unitOfWork.Save());

            return new LineItemModel(lineItem);
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            unitOfWork.OrderRepository.Delete(id);

        }
    }
}
