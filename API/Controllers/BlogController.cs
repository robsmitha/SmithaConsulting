using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataLayer.Data;
using DataLayer.Repositories;
using DomainLayer.Models;
using AutoMapper;
using DataLayer.Entities;
using DomainLayer.BLL;
using System;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly BusinessLogic BLL;
        public BlogController(DbArchitecture context, IMapper mapper)
        {
            if (BLL == null)
            {
                BLL = new BusinessLogic(context, mapper);
            }
        }

        // GET: api/Blogs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BlogModel>>> GetBlogs()
        {
            try
            {
                var blogs = await BLL.Blogs.GetAllAsync();
                return Ok(blogs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        // GET: api/Blogs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BlogModel>> GetBlog(int id)
        {
            try
            {
                var blog = await BLL.Blogs.GetAsync(id);
                if (blog == null)
                {
                    return NotFound();
                }
                return Ok(blog);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
