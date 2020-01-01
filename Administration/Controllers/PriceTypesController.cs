using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataLayer.Data;
using DomainLayer.Entities;

namespace Administration.Controllers
{
    public class PriceTypesController : BaseController
    {
        private readonly OperationsContext _context;

        public PriceTypesController(OperationsContext context) : base(context)
        {
            _context = context;
        }

        // GET: PriceTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.PriceTypes.ToListAsync());
        }

        // GET: PriceTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var priceType = await _context.PriceTypes
                .FirstOrDefaultAsync(m => m.ID == id);
            if (priceType == null)
            {
                return NotFound();
            }

            return View(priceType);
        }

        // GET: PriceTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PriceTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,ID,CreatedAt,Active,ModifiedTime")] PriceType priceType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(priceType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(priceType);
        }

        // GET: PriceTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var priceType = await _context.PriceTypes.FindAsync(id);
            if (priceType == null)
            {
                return NotFound();
            }
            return View(priceType);
        }

        // POST: PriceTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Description,ID,CreatedAt,Active,ModifiedTime")] PriceType priceType)
        {
            if (id != priceType.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(priceType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PriceTypeExists(priceType.ID))
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
            return View(priceType);
        }

        // GET: PriceTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var priceType = await _context.PriceTypes
                .FirstOrDefaultAsync(m => m.ID == id);
            if (priceType == null)
            {
                return NotFound();
            }

            return View(priceType);
        }

        // POST: PriceTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var priceType = await _context.PriceTypes.FindAsync(id);
            _context.PriceTypes.Remove(priceType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PriceTypeExists(int id)
        {
            return _context.PriceTypes.Any(e => e.ID == id);
        }
    }
}
