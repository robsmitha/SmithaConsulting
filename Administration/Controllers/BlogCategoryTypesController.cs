using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Architecture;
using Architecture.Data;

namespace Administration.Controllers
{
    public class BlogCategoryTypesController : Controller
    {
        private readonly DbArchitecture _context;

        public BlogCategoryTypesController(DbArchitecture context)
        {
            _context = context;
        }

        // GET: BlogCategoryTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.BlogCategoryTypes.ToListAsync());
        }

        // GET: BlogCategoryTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogCategoryType = await _context.BlogCategoryTypes
                .FirstOrDefaultAsync(m => m.ID == id);
            if (blogCategoryType == null)
            {
                return NotFound();
            }

            return View(blogCategoryType);
        }

        // GET: BlogCategoryTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BlogCategoryTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,ID,CreatedAt,Active,ModifiedTime")] BlogCategoryType blogCategoryType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(blogCategoryType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(blogCategoryType);
        }

        // GET: BlogCategoryTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogCategoryType = await _context.BlogCategoryTypes.FindAsync(id);
            if (blogCategoryType == null)
            {
                return NotFound();
            }
            return View(blogCategoryType);
        }

        // POST: BlogCategoryTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Description,ID,CreatedAt,Active,ModifiedTime")] BlogCategoryType blogCategoryType)
        {
            if (id != blogCategoryType.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(blogCategoryType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogCategoryTypeExists(blogCategoryType.ID))
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
            return View(blogCategoryType);
        }

        // GET: BlogCategoryTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogCategoryType = await _context.BlogCategoryTypes
                .FirstOrDefaultAsync(m => m.ID == id);
            if (blogCategoryType == null)
            {
                return NotFound();
            }

            return View(blogCategoryType);
        }

        // POST: BlogCategoryTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blogCategoryType = await _context.BlogCategoryTypes.FindAsync(id);
            _context.BlogCategoryTypes.Remove(blogCategoryType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlogCategoryTypeExists(int id)
        {
            return _context.BlogCategoryTypes.Any(e => e.ID == id);
        }
    }
}
