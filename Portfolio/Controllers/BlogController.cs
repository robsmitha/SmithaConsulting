using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DomainLayer.Models;
using System.Collections.Generic;
using DomainLayer.Services;
using Portfolio.Models;
using System.Linq;
using AutoMapper;

namespace Portfolio.Controllers
{
    public class BlogController : BaseController
    {
        public BlogController(IApiService api, IMapper mapper, ICacheService cache) : base(api, mapper, cache) { }
        // GET: Blog
        public async Task<IActionResult> Index()
        {
            var blogs = await _api.GetAsync<IEnumerable<BlogModel>>("/blog");            
            return View(new BlogListViewModel(_mapper.Map<IEnumerable<BlogViewModel>>(blogs)));
        }

        // GET: Blog/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _api.GetAsync<BlogModel>($"/blog/{id}");
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

    }
}
