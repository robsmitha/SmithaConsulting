using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DomainLayer.Models;
using System.Collections.Generic;

namespace Portfolio.Controllers
{
    public class BlogController : BaseController
    {

        // GET: Blog
        public async Task<IActionResult> Index()
        {
            return View(await API.GetAsync<IEnumerable<BlogModel>>("/blog"));
        }

        // GET: Blog/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await API.GetAsync<BlogModel>($"/blog/{id}");
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

    }
}
