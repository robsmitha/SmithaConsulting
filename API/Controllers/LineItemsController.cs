using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class LineItemsController : ControllerBase
    {
        private readonly BusinessLogic BLL;
        public LineItemsController(DbArchitecture context, IMapper mapper)
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
