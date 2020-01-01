using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataLayer.Data;
using DomainLayer.Entities;

namespace Administration.Controllers
{
    public class BlogStatusTypesController : Controller
    {
        private readonly OperationsContext _context;

        public BlogStatusTypesController(OperationsContext context)
        {
            _context = context;
        }

        // GET: BlogStatusTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.BlogStatusTypes.ToListAsync());
        }

        // GET: BlogStatusTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogStatusType = await _context.BlogStatusTypes
                .FirstOrDefaultAsync(m => m.ID == id);
            if (blogStatusType == null)
            {
                return NotFound();
            }

            return View(blogStatusType);
        }

        // GET: BlogStatusTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BlogStatusTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,ID,CreatedAt,Active,ModifiedTime")] BlogStatusType blogStatusType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(blogStatusType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(blogStatusType);
        }

        // GET: BlogStatusTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogStatusType = await _context.BlogStatusTypes.FindAsync(id);
            if (blogStatusType == null)
            {
                return NotFound();
            }
            return View(blogStatusType);
        }

        // POST: BlogStatusTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Description,ID,CreatedAt,Active,ModifiedTime")] BlogStatusType blogStatusType)
        {
            if (id != blogStatusType.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(blogStatusType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogStatusTypeExists(blogStatusType.ID))
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
            return View(blogStatusType);
        }

        // GET: BlogStatusTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogStatusType = await _context.BlogStatusTypes
                .FirstOrDefaultAsync(m => m.ID == id);
            if (blogStatusType == null)
            {
                return NotFound();
            }

            return View(blogStatusType);
        }

        // POST: BlogStatusTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blogStatusType = await _context.BlogStatusTypes.FindAsync(id);
            _context.BlogStatusTypes.Remove(blogStatusType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlogStatusTypeExists(int id)
        {
            return _context.BlogStatusTypes.Any(e => e.ID == id);
        }
    }
}
