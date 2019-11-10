using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
    public class ApplicationsController : ControllerBase
    {
        private readonly UnitOfWork unitOfWork;
        public ApplicationsController(DbArchitecture context)
        {
            unitOfWork = new UnitOfWork(context);
        }
        // GET: api/Applications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplicationDTO>>> GetApplications()
        {
            var applications = await unitOfWork
                .ApplicationRepository
                .GetAllAsync(includeProperties: "ApplicationType");

            return Ok(applications.Select(x => new ApplicationDTO(x)).ToArray());
        }

        // GET: api/Applications/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationDTO>> GetApplication(int id)
        {
            var application = await unitOfWork
                .ApplicationRepository
                .GetAsync(x => x.ID == id, includeProperties: "ApplicationType");
            
            if (application == null)
            {
                return NotFound();
            }

            var dto = new ApplicationDTO(application);
            return Ok(dto);
        }

        // PUT: api/Applications/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApplication(int id, ApplicationDTO dto)
        {
            if (id != dto.ID)
            {
                return BadRequest();
            }
            var application = unitOfWork
                .ApplicationRepository
                .GetAll(x => x.ID == id, includeProperties: "ApplicationType").FirstOrDefault();
            
            if(application == null)
            {
                return NotFound();
            }

            unitOfWork.ApplicationRepository.Update(application);

            try
            {
                await unitOfWork.SaveAsync();
                return Ok();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500);
            }
        }
    }
}
