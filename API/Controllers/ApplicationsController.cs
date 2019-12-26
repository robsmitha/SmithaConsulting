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
using DataLayer.DAL;
using AutoMapper;
using DomainLayer.BLL;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationsController : ControllerBase
    {
        private readonly BusinessLogic BLL;
        public ApplicationsController(DbArchitecture context, IMapper mapper)
        {
            if (BLL == null)
            {
                BLL = new BusinessLogic(context, mapper);
            }
        }
        // GET: api/Applications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplicationModel>>> GetApplications()
        {
            var applications = await BLL.Applications.GetAllAsync();
            return Ok(applications);
        }

        // GET: api/Applications/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationModel>> GetApplication(int id)
        {
            try
            {
                var application = await BLL.Applications.GetAsync(id);
                if (application == null)
                {
                    return NotFound();
                }
                return Ok(application);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        // GET: api/Applications/store
        [HttpGet("GetByName/{name}")]
        public async Task<ActionResult<ApplicationModel>> GetApplicationByName(string name)
        {
            try
            {
                var application = await BLL.Applications.GetByNameAsync(name);
                if (application == null)
                {
                    return NotFound();
                }
                return Ok(application);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        // PUT: api/Applications/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ApplicationModel>> PutApplication(int id, ApplicationModel model)
        {
            if (id != model.ID)
            {
                return BadRequest();
            }
            try
            {
                var application = await BLL.Applications.UpdateAsync(model);
                if (application == null)
                {
                    return NotFound();
                }
                return Ok(application);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        // POST: api/Applications
        [HttpPost]
        public async Task<ActionResult<ApplicationModel>> PostApplication(ApplicationModel model)
        {
            try
            {
                return Ok(await BLL.Applications.AddAsync(model));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            try
            {
                await BLL.Applications.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
