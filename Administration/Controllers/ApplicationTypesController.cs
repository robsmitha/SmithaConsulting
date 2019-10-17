using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataModeling;
using DataModeling.Data;

namespace Administration.Controllers
{
    public class ApplicationTypesController : BaseController
    {
        private readonly DbArchitecture _context;

        public ApplicationTypesController(DbArchitecture context) : base(context)
        {
            _context = context;
        }

        // GET: ApplicationTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.ApplicationTypes.ToListAsync());
        }

        // GET: ApplicationTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationType = await _context.ApplicationTypes
                .FirstOrDefaultAsync(m => m.ID == id);
            if (applicationType == null)
            {
                return NotFound();
            }

            return View(applicationType);
        }

        // GET: ApplicationTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ApplicationTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,ID,CreatedAt,Active,ModifiedTime")] ApplicationType applicationType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(applicationType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(applicationType);
        }

        // GET: ApplicationTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationType = await _context.ApplicationTypes.FindAsync(id);
            if (applicationType == null)
            {
                return NotFound();
            }
            return View(applicationType);
        }

        // POST: ApplicationTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Description,ID,CreatedAt,Active,ModifiedTime")] ApplicationType applicationType)
        {
            if (id != applicationType.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applicationType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationTypeExists(applicationType.ID))
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
            return View(applicationType);
        }

        // GET: ApplicationTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationType = await _context.ApplicationTypes
                .FirstOrDefaultAsync(m => m.ID == id);
            if (applicationType == null)
            {
                return NotFound();
            }

            return View(applicationType);
        }

        // POST: ApplicationTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var applicationType = await _context.ApplicationTypes.FindAsync(id);
            _context.ApplicationTypes.Remove(applicationType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicationTypeExists(int id)
        {
            return _context.ApplicationTypes.Any(e => e.ID == id);
        }
    }
}
