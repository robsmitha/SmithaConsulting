using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Architecture;
using Architecture.Data;
using Architecture.DTOs;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationsController : ControllerBase
    {
        private readonly DbArchitecture _context;

        public ApplicationsController(DbArchitecture context)
        {
            _context = context;
        }

        // GET: api/Applications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplicationDTO>>> GetApplications()
        {
            var applications = _context.Applications
                .Include(o => o.ApplicationType);
            return await applications.Select(x=> new ApplicationDTO(x)).ToListAsync();
        }

        // GET: api/Applications/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationDTO>> GetApplication(int id)
        {
            var application = await _context.Applications.FindAsync(id);

            if (application == null)
            {
                return NotFound();
            }

            var dto = new ApplicationDTO(application);
            return dto;
        }

        // PUT: api/Applications/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApplication(int id, ApplicationDTO dto)
        {
            if (id != dto.ID)
            {
                return BadRequest();
            }

            var application = await _context.Applications.SingleOrDefaultAsync(x => x.ID == id);

            _context.Entry(application).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Applications
        [HttpPost]
        public async Task<ActionResult<ApplicationDTO>> PostApplication(ApplicationDTO dto)
        {
            var application = new Application
            {
                ApplicationTypeID = dto.ApplicationTypeID,
                Description = dto.Description,
                Name = dto.Name
            };
            _context.Applications.Add(application);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetApplication", new { id = dto.ID }, dto);
        }

        // DELETE: api/Applications/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Application>> DeleteApplication(int id)
        {
            var application = await _context.Applications.FindAsync(id);
            if (application == null)
            {
                return NotFound();
            }

            _context.Applications.Remove(application);
            await _context.SaveChangesAsync();

            return application;
        }

        private bool ApplicationExists(int id)
        {
            return _context.Applications.Any(e => e.ID == id);
        }
    }
}
