using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataModeling;
using DataModeling.Data;
using Architecture.DAL;
using Architecture.DTOs;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly UnitOfWork unitOfWork;
        public BlogController(DbArchitecture context)
        {
            unitOfWork = new UnitOfWork(context);
        }


        // GET: api/Blogs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BlogDTO>>> GetBlogs()
        {
            var blog = unitOfWork.BlogRepository
                .Get(includeProperties: "BlogStatusType,User")
                .Select(x => new BlogDTO(x));
            return await System.Threading.Tasks.Task.Run(() => blog.ToList());
        }

        // GET: api/Blogs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BlogDTO>> GetBlog(int id)
        {
            var blog = await System.Threading.Tasks.Task.Run(() => unitOfWork.BlogRepository
                .Get(x => x.ID == id, includeProperties: "BlogStatusType,User")
                .Select(x => new BlogDTO(x))
                .SingleOrDefault());

            if (blog == null)
            {
                return NotFound();
            }

            return blog;
        }
    }
}
