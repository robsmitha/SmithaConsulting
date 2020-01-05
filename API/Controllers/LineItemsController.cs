using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Data;
using Domain.Models;
using AutoMapper;
using API.BLL;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LineItemsController : ControllerBase
    {
        private readonly BusinessLogic BLL;
        public LineItemsController(OperationsContext context, IMapper mapper)
        {
            if (BLL == null)
            {
                BLL = new BusinessLogic(context, mapper);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LineItemModel>>> Get()
        {
            try
            {
                var lineItems = await BLL.LineItems.GetAllAsync();
                return Ok(lineItems);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<LineItemModel>> Get(int id)
        {
            try
            {
                var lineItem = await BLL.LineItems.GetAsync(id);
                if (lineItem == null)
                {
                    return NotFound();
                }
                return Ok(lineItem);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<LineItemModel>> Put(int id, LineItemModel model)
        {
            if (id != model.ID)
            {
                return BadRequest();
            }
            try
            {
                var lineItem = await BLL.LineItems.UpdateAsync(model);
                if (lineItem == null)
                {
                    return NotFound();
                }
                return Ok(lineItem);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }


        [HttpPost]
        public async Task<ActionResult<LineItemModel>> Post(LineItemModel model)
        {
            try
            {
                return Ok(await BLL.LineItems.AddAsync(model));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await BLL.LineItems.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
