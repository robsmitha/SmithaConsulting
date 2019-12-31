using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataLayer.Entities;
using DataLayer.Data;

namespace Administration.Controllers
{
    public class PaymentStatusTypesController : BaseController
    {
        private readonly DbArchitecture _context;

        public PaymentStatusTypesController(DbArchitecture context) : base(context)
        {
            _context = context;
        }

        // GET: PaymentStatusTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.PaymentStatusTypes.ToListAsync());
        }

        // GET: PaymentStatusTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentStatusType = await _context.PaymentStatusTypes
                .FirstOrDefaultAsync(m => m.ID == id);
            if (paymentStatusType == null)
            {
                return NotFound();
            }

            return View(paymentStatusType);
        }

        // GET: PaymentStatusTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PaymentStatusTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,ID,CreatedAt,Active,ModifiedTime")] PaymentStatusType paymentStatusType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(paymentStatusType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(paymentStatusType);
        }

        // GET: PaymentStatusTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentStatusType = await _context.PaymentStatusTypes.FindAsync(id);
            if (paymentStatusType == null)
            {
                return NotFound();
            }
            return View(paymentStatusType);
        }

        // POST: PaymentStatusTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Description,ID,CreatedAt,Active,ModifiedTime")] PaymentStatusType paymentStatusType)
        {
            if (id != paymentStatusType.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paymentStatusType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentStatusTypeExists(paymentStatusType.ID))
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
            return View(paymentStatusType);
        }

        // GET: PaymentStatusTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentStatusType = await _context.PaymentStatusTypes
                .FirstOrDefaultAsync(m => m.ID == id);
            if (paymentStatusType == null)
            {
                return NotFound();
            }

            return View(paymentStatusType);
        }

        // POST: PaymentStatusTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var paymentStatusType = await _context.PaymentStatusTypes.FindAsync(id);
            _context.PaymentStatusTypes.Remove(paymentStatusType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentStatusTypeExists(int id)
        {
            return _context.PaymentStatusTypes.Any(e => e.ID == id);
        }
    }
}
