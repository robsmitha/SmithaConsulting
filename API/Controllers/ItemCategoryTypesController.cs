using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.BLL;
using AutoMapper;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemCategoryTypesController : ControllerBase
    {
        private readonly BusinessLogic BLL;
        public ItemCategoryTypesController(OperationsContext context, IMapper mapper)
        {
            if (BLL == null)
            {
                BLL = new BusinessLogic(context, mapper);
            }
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemCategoryTypeModel>>> Get()
        {
            try
            {
                var collection = await BLL.ItemCategoryTypes.GetAllAsync();
                return Ok(collection);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}