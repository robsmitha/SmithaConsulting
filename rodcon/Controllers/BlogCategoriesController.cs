using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using rod;
using rod.Data;

namespace rodcon.Controllers
{
    public class BlogCategoriesController : Controller
    {
        private readonly rodContext _context;

        public BlogCategoriesController(rodContext context)
        {
            _context = context;
        }

        // GET: BlogCategories
        public async Task<IActionResult> Index()
        {
            var rodContext = _context.BlogCategories.Include(b => b.Blog).Include(b => b.BlogCategoryType);
            return View(await rodContext.ToListAsync());
        }

        // GET: BlogCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogCategory = await _context.BlogCategories
                .Include(b => b.Blog)
                .Include(b => b.BlogCategoryType)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (blogCategory == null)
            {
                return NotFound();
            }

            return View(blogCategory);
        }

        // GET: BlogCategories/Create
        public IActionResult Create()
        {
            ViewData["BlogID"] = new SelectList(_context.Blogs, "ID", "ID");
            ViewData["BlogCategoryTypeID"] = new SelectList(_context.BlogCategoryTypes, "ID", "ID");
            return View();
        }

        // POST: BlogCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BlogCategoryTypeID,BlogID,ID,CreatedAt,Active,ModifiedTime")] BlogCategory blogCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(blogCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BlogID"] = new SelectList(_context.Blogs, "ID", "ID", blogCategory.BlogID);
            ViewData["BlogCategoryTypeID"] = new SelectList(_context.BlogCategoryTypes, "ID", "ID", blogCategory.BlogCategoryTypeID);
            return View(blogCategory);
        }

        // GET: BlogCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogCategory = await _context.BlogCategories.FindAsync(id);
            if (blogCategory == null)
            {
                return NotFound();
            }
            ViewData["BlogID"] = new SelectList(_context.Blogs, "ID", "ID", blogCategory.BlogID);
            ViewData["BlogCategoryTypeID"] = new SelectList(_context.BlogCategoryTypes, "ID", "ID", blogCategory.BlogCategoryTypeID);
            return View(blogCategory);
        }

        // POST: BlogCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BlogCategoryTypeID,BlogID,ID,CreatedAt,Active,ModifiedTime")] BlogCategory blogCategory)
        {
            if (id != blogCategory.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(blogCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogCategoryExists(blogCategory.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BlogID"] = new SelectList(_context.Blogs, "ID", "ID", blogCategory.BlogID);
            ViewData["BlogCategoryTypeID"] = new SelectList(_context.BlogCategoryTypes, "ID", "ID", blogCategory.BlogCategoryTypeID);
            return View(blogCategory);
        }

        // GET: BlogCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogCategory = await _context.BlogCategories
                .Include(b => b.Blog)
                .Include(b => b.BlogCategoryType)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (blogCategory == null)
            {
                return NotFound();
            }

            return View(blogCategory);
        }

        // POST: BlogCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blogCategory = await _context.BlogCategories.FindAsync(id);
            _context.BlogCategories.Remove(blogCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlogCategoryExists(int id)
        {
            return _context.BlogCategories.Any(e => e.ID == id);
        }
    }
}
