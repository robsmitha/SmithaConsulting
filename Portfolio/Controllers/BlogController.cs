using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Architecture.DTOs;

namespace Portfolio.Controllers
{
    public class BlogController : BaseController
    {

        // GET: Blog
        public async Task<IActionResult> Index()
        {
            return View(await API.GetAllAsync<BlogDTO>("/blogs"));
        }

        // GET: Blog/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await API.GetAsync<BlogDTO>($"/blogs/{id}");
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

    }
}
