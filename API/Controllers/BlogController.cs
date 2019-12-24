using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataLayer.Data;
using DataLayer.DAL;
using DomainLayer.Models;
using AutoMapper;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly UnitOfWork unitOfWork;
        private readonly IMapper _mapper;
        public BlogController(DbArchitecture context, IMapper mapper)
        {
            unitOfWork = new UnitOfWork(context);
            _mapper = mapper;
        }

        // GET: api/Blogs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BlogModel>>> GetBlogs()
        {
            var blogs = await unitOfWork
                .BlogRepository
                .GetAllAsync(includeProperties: "BlogStatusType,User");

            return Ok(_mapper.Map<IEnumerable<BlogModel>>(blogs));
        }

        // GET: api/Blogs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BlogModel>> GetBlog(int id)
        {
            var blog = await unitOfWork
                .BlogRepository
                .GetAsync(x => x.ID == id, includeProperties: "BlogStatusType,User");

            if (blog == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<BlogModel>(blog));
        }
    }
}
