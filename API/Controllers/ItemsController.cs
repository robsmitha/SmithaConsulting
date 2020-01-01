using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.BLL;
using AutoMapper;
using DataLayer.Data;
using DomainLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : Controller
    {
        private readonly BusinessLogic BLL;
        public ItemsController(OperationsContext context, IMapper mapper)
        {
            if (BLL == null)
            {
                BLL = new BusinessLogic(context, mapper);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemModel>>> Get()
        {
            try
            {
                var items = await BLL.Items.GetAllAsync();
                return Ok(items);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemModel>> Get(int id)
        {
            try
            {
                var item = await BLL.Items.GetAsync(id);
                if (item == null)
                {
                    return NotFound();
                }

                return Ok(item);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ItemModel>> Post([FromBody] ItemModel model)
        {
            try
            {
                return Ok(await BLL.Items.AddAsync(model));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ItemModel>> Put(int id, [FromBody] ItemModel model)
        {
            if (id != model.ID)
            {
                return BadRequest();
            }
            try
            {
                var item = await BLL.Items.UpdateAsync(model);

                if (item == null)
                {
                    return NotFound();
                }
                return Ok(item);
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
                await BLL.Items.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}