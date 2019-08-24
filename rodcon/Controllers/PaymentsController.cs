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
    public class PaymentsController : BaseController
    {
        private readonly rodContext _context;

        public PaymentsController(rodContext context) : base(context)
        {
            _context = context;
        }

        // GET: Payments
        public async Task<IActionResult> Index()
        {
            var rodContext = _context.Payments.Include(p => p.Authorization).Include(p => p.Order).Include(p => p.PaymentStatusType).Include(p => p.PaymentType);
            return View(await rodContext.ToListAsync());
        }

        // GET: Payments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments
                .Include(p => p.Authorization)
                .Include(p => p.Order)
                .Include(p => p.PaymentStatusType)
                .Include(p => p.PaymentType)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // GET: Payments/Create
        public IActionResult Create()
        {
            ViewData["AuthorizationID"] = new SelectList(_context.Authorizations, "ID", "ID");
            ViewData["OrderID"] = new SelectList(_context.Orders, "ID", "ID");
            ViewData["PaymentStatusTypeID"] = new SelectList(_context.PaymentStatusTypes, "ID", "ID");
            ViewData["PaymentTypeID"] = new SelectList(_context.PaymentTypes, "ID", "Description");
            return View();
        }

        // POST: Payments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Amount,CashTendered,PaymentTypeID,PaymentStatusTypeID,AuthorizationID,OrderID,ID,CreatedAt,Active,ModifiedTime")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(payment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorizationID"] = new SelectList(_context.Authorizations, "ID", "ID", payment.AuthorizationID);
            ViewData["OrderID"] = new SelectList(_context.Orders, "ID", "ID", payment.OrderID);
            ViewData["PaymentStatusTypeID"] = new SelectList(_context.PaymentStatusTypes, "ID", "ID", payment.PaymentStatusTypeID);
            ViewData["PaymentTypeID"] = new SelectList(_context.PaymentTypes, "ID", "Description", payment.PaymentTypeID);
            return View(payment);
        }

        // GET: Payments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }
            ViewData["AuthorizationID"] = new SelectList(_context.Authorizations, "ID", "ID", payment.AuthorizationID);
            ViewData["OrderID"] = new SelectList(_context.Orders, "ID", "ID", payment.OrderID);
            ViewData["PaymentStatusTypeID"] = new SelectList(_context.PaymentStatusTypes, "ID", "ID", payment.PaymentStatusTypeID);
            ViewData["PaymentTypeID"] = new SelectList(_context.PaymentTypes, "ID", "Description", payment.PaymentTypeID);
            return View(payment);
        }

        // POST: Payments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Amount,CashTendered,PaymentTypeID,PaymentStatusTypeID,AuthorizationID,OrderID,ID,CreatedAt,Active,ModifiedTime")] Payment payment)
        {
            if (id != payment.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(payment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentExists(payment.ID))
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
            ViewData["AuthorizationID"] = new SelectList(_context.Authorizations, "ID", "ID", payment.AuthorizationID);
            ViewData["OrderID"] = new SelectList(_context.Orders, "ID", "ID", payment.OrderID);
            ViewData["PaymentStatusTypeID"] = new SelectList(_context.PaymentStatusTypes, "ID", "ID", payment.PaymentStatusTypeID);
            ViewData["PaymentTypeID"] = new SelectList(_context.PaymentTypes, "ID", "Description", payment.PaymentTypeID);
            return View(payment);
        }

        // GET: Payments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments
                .Include(p => p.Authorization)
                .Include(p => p.Order)
                .Include(p => p.PaymentStatusType)
                .Include(p => p.PaymentType)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentExists(int id)
        {
            return _context.Payments.Any(e => e.ID == id);
        }
    }
}
