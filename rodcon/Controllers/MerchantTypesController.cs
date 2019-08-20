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
    public class MerchantTypesController : BaseController
    {
        private readonly rodContext _context;

        public MerchantTypesController(rodContext context)
        {
            _context = context;
        }

        // GET: MerchantTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.MerchantTypes.ToListAsync());
        }

        // GET: MerchantTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var merchantType = await _context.MerchantTypes
                .FirstOrDefaultAsync(m => m.ID == id);
            if (merchantType == null)
            {
                return NotFound();
            }

            return View(merchantType);
        }

        // GET: MerchantTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MerchantTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,ID,CreatedAt,Active,ModifiedTime")] MerchantType merchantType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(merchantType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(merchantType);
        }

        // GET: MerchantTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var merchantType = await _context.MerchantTypes.FindAsync(id);
            if (merchantType == null)
            {
                return NotFound();
            }
            return View(merchantType);
        }

        // POST: MerchantTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Description,ID,CreatedAt,Active,ModifiedTime")] MerchantType merchantType)
        {
            if (id != merchantType.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(merchantType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MerchantTypeExists(merchantType.ID))
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
            return View(merchantType);
        }

        // GET: MerchantTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var merchantType = await _context.MerchantTypes
                .FirstOrDefaultAsync(m => m.ID == id);
            if (merchantType == null)
            {
                return NotFound();
            }

            return View(merchantType);
        }

        // POST: MerchantTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var merchantType = await _context.MerchantTypes.FindAsync(id);
            _context.MerchantTypes.Remove(merchantType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MerchantTypeExists(int id)
        {
            return _context.MerchantTypes.Any(e => e.ID == id);
        }
    }
}
