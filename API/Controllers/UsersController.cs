using System;
using System.Collections.Generic;
using System.Linq;
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
    public class UsersController : ControllerBase
    {
        private readonly BusinessLogic BLL;

        public UsersController(OperationsContext context, IMapper mapper)
        {
            if (BLL == null)
            {
                BLL = new BusinessLogic(context, mapper);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserModel>>> Get()
        {
            try
            {
                var collection = await BLL.Users.GetAllAsync();
                return Ok(collection);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> Get(int id)
        {
            try
            {
                var model = await BLL.Users.GetAsync(id);
                if (model == null)
                {
                    return NotFound();
                }

                return Ok(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("GetByUsername/{username}")]
        public async Task<ActionResult<UserModel>> GetByUsername(string username)
        {
            try
            {
                var model = await BLL.Users.GetByUsernameAsync(username);
                if (model == null)
                {
                    return NotFound();
                }

                return Ok(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserModel>> Put(int id, UserModel model)
        {
            if (id != model.ID)
            {
                return BadRequest();
            }
            try
            {
                var result = await BLL.Users.UpdateAsync(model);

                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        public async Task<ActionResult<UserModel>> Post(UserModel model)
        {
            try
            {
                return Ok(await BLL.Users.AddAsync(model));
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
                await BLL.Users.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}