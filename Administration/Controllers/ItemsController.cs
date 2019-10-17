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
    public class ItemsController : BaseController
    {
        private readonly DbArchitecture _context;

        public ItemsController(DbArchitecture context) : base(context)
        {
            _context = context;
        }

        // GET: Items
        public async Task<IActionResult> Index(int? merchantId)
        {
            merchantId = merchantId ?? MerchantID;
            var merchantItems = _context.Items
                .Where(x => x.MerchantID == merchantId)
                .Include(i => i.ItemType)
                .Include(i => i.Merchant)
                .Include(i => i.PriceType)
                .Include(i => i.UnitType);
            return View(await merchantItems.ToListAsync());
        }

        // GET: Items/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .Include(i => i.ItemType)
                .Include(i => i.Merchant)
                .Include(i => i.PriceType)
                .Include(i => i.UnitType)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Items/Create
        public IActionResult Create()
        {
            ViewData["ItemTypeID"] = new SelectList(_context.ItemTypes, "ID", "Description");
            ViewData["MerchantID"] = new SelectList(_context.Merchants, "ID", "MerchantName");
            ViewData["PriceTypeID"] = new SelectList(_context.PriceTypes, "ID", "Description");
            ViewData["UnitTypeID"] = new SelectList(_context.UnitTypes, "ID", "Description");
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemName,ItemDescription,ItemTypeID,MerchantID,Cost,Price,PriceTypeID,UnitTypeID,Code,Sku,DefaultTaxRates,IsRevenue,LookupCode,Percentage,ID,CreatedAt,Active,ModifiedTime")] Item item)
        {
            if (ModelState.IsValid)
            {
                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ItemTypeID"] = new SelectList(_context.ItemTypes, "ID", "Description", item.ItemTypeID);
            ViewData["MerchantID"] = new SelectList(_context.Merchants, "ID", "MerchantName", item.MerchantID);
            ViewData["PriceTypeID"] = new SelectList(_context.PriceTypes, "ID", "Description", item.PriceTypeID);
            ViewData["UnitTypeID"] = new SelectList(_context.UnitTypes, "ID", "Description", item.UnitTypeID);
            return View(item);
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            ViewData["ItemTypeID"] = new SelectList(_context.ItemTypes, "ID", "Description", item.ItemTypeID);
            ViewData["MerchantID"] = new SelectList(_context.Merchants, "ID", "MerchantName", item.MerchantID);
            ViewData["PriceTypeID"] = new SelectList(_context.PriceTypes, "ID", "Description", item.PriceTypeID);
            ViewData["UnitTypeID"] = new SelectList(_context.UnitTypes, "ID", "Description", item.UnitTypeID);
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ItemName,ItemDescription,ItemTypeID,MerchantID,Cost,Price,PriceTypeID,UnitTypeID,Code,Sku,DefaultTaxRates,IsRevenue,LookupCode,Percentage,ID,CreatedAt,Active,ModifiedTime")] Item item)
        {
            if (id != item.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.ID))
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
            ViewData["ItemTypeID"] = new SelectList(_context.ItemTypes, "ID", "Description", item.ItemTypeID);
            ViewData["MerchantID"] = new SelectList(_context.Merchants, "ID", "MerchantName", item.MerchantID);
            ViewData["PriceTypeID"] = new SelectList(_context.PriceTypes, "ID", "Description", item.PriceTypeID);
            ViewData["UnitTypeID"] = new SelectList(_context.UnitTypes, "ID", "Description", item.UnitTypeID);
            return View(item);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .Include(i => i.ItemType)
                .Include(i => i.Merchant)
                .Include(i => i.PriceType)
                .Include(i => i.UnitType)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.Items.FindAsync(id);
            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.ID == id);
        }
    }
}
