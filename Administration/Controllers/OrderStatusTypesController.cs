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
    public class OrderStatusTypesController : BaseController
    {
        private readonly DbArchitecture _context;

        public OrderStatusTypesController(DbArchitecture context) : base(context)
        {
            _context = context;
        }

        // GET: OrderStatusTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.OrderStatusTypes.ToListAsync());
        }

        // GET: OrderStatusTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderStatusType = await _context.OrderStatusTypes
                .FirstOrDefaultAsync(m => m.ID == id);
            if (orderStatusType == null)
            {
                return NotFound();
            }

            return View(orderStatusType);
        }

        // GET: OrderStatusTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OrderStatusTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,ID,CreatedAt,Active,ModifiedTime")] OrderStatusType orderStatusType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderStatusType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(orderStatusType);
        }

        // GET: OrderStatusTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderStatusType = await _context.OrderStatusTypes.FindAsync(id);
            if (orderStatusType == null)
            {
                return NotFound();
            }
            return View(orderStatusType);
        }

        // POST: OrderStatusTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Description,ID,CreatedAt,Active,ModifiedTime")] OrderStatusType orderStatusType)
        {
            if (id != orderStatusType.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderStatusType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderStatusTypeExists(orderStatusType.ID))
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
            return View(orderStatusType);
        }

        // GET: OrderStatusTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderStatusType = await _context.OrderStatusTypes
                .FirstOrDefaultAsync(m => m.ID == id);
            if (orderStatusType == null)
            {
                return NotFound();
            }

            return View(orderStatusType);
        }

        // POST: OrderStatusTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderStatusType = await _context.OrderStatusTypes.FindAsync(id);
            _context.OrderStatusTypes.Remove(orderStatusType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderStatusTypeExists(int id)
        {
            return _context.OrderStatusTypes.Any(e => e.ID == id);
        }
    }
}
