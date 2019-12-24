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

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationsController : ControllerBase
    {
        private readonly UnitOfWork unitOfWork;
        private readonly IMapper _mapper;
        public ApplicationsController(DbArchitecture context, IMapper mapper)
        {
            unitOfWork = new UnitOfWork(context);
            _mapper = mapper;
        }
        // GET: api/Applications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplicationModel>>> GetApplications()
        {
            var applications = await unitOfWork
                .ApplicationRepository
                .GetAllAsync(includeProperties: "ApplicationType");

            return Ok(_mapper.Map<IEnumerable<ApplicationModel>>(applications));
        }

        // GET: api/Applications/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationModel>> GetApplication(int id)
        {
            var application = await unitOfWork
                .ApplicationRepository
                .GetAsync(x => x.ID == id, includeProperties: "ApplicationType");
            
            if (application == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ApplicationModel>(application));
        }
        // GET: api/Applications/store
        [HttpGet("GetByName/{name}")]
        public async Task<ActionResult<ApplicationModel>> GetApplicationByName(string name)
        {
            var application = await unitOfWork
                .ApplicationRepository
                .GetAsync(x => x.Name.ToLower() == name.ToLower(), includeProperties: "ApplicationType");

            if (application == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ApplicationModel>(application));
        }

        // PUT: api/Applications/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApplication(int id, ApplicationModel model)
        {
            if (id != model.ID)
            {
                return BadRequest();
            }
            var application = unitOfWork.ApplicationRepository.Get(x => x.ID == id, includeProperties: "ApplicationType");
            
            if(application == null)
            {
                return NotFound();
            }
            _mapper.Map(model, application);
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
