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
    public class MerchantUsersController : BaseController
    {
        private readonly rodContext _context;

        public MerchantUsersController(rodContext context) : base(context)
        {
            _context = context;
        }

        // GET: MerchantUsers
        public async Task<IActionResult> Index()
        {
            var rodContext = _context.MerchantUsers.Include(m => m.Merchant).Include(m => m.Role).Include(m => m.User);
            return View(await rodContext.ToListAsync());
        }

        // GET: MerchantUsers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var merchantUser = await _context.MerchantUsers
                .Include(m => m.Merchant)
                .Include(m => m.Role)
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (merchantUser == null)
            {
                return NotFound();
            }

            return View(merchantUser);
        }

        // GET: MerchantUsers/Create
        public IActionResult Create()
        {
            ViewData["MerchantID"] = new SelectList(_context.Merchants, "ID", "MerchantName");
            ViewData["RoleID"] = new SelectList(_context.Roles, "ID", "Description");
            ViewData["UserID"] = new SelectList(_context.Users, "ID", "Email");
            return View();
        }

        // POST: MerchantUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MerchantID,UserID,RoleID,ID,CreatedAt,Active,ModifiedTime")] MerchantUser merchantUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(merchantUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MerchantID"] = new SelectList(_context.Merchants, "ID", "MerchantName", merchantUser.MerchantID);
            ViewData["RoleID"] = new SelectList(_context.Roles, "ID", "Description", merchantUser.RoleID);
            ViewData["UserID"] = new SelectList(_context.Users, "ID", "Email", merchantUser.UserID);
            return View(merchantUser);
        }

        // GET: MerchantUsers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var merchantUser = await _context.MerchantUsers.FindAsync(id);
            if (merchantUser == null)
            {
                return NotFound();
            }
            ViewData["MerchantID"] = new SelectList(_context.Merchants, "ID", "MerchantName", merchantUser.MerchantID);
            ViewData["RoleID"] = new SelectList(_context.Roles, "ID", "Description", merchantUser.RoleID);
            ViewData["UserID"] = new SelectList(_context.Users, "ID", "Email", merchantUser.UserID);
            return View(merchantUser);
        }

        // POST: MerchantUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MerchantID,UserID,RoleID,ID,CreatedAt,Active,ModifiedTime")] MerchantUser merchantUser)
        {
            if (id != merchantUser.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(merchantUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MerchantUserExists(merchantUser.ID))
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
            ViewData["MerchantID"] = new SelectList(_context.Merchants, "ID", "MerchantName", merchantUser.MerchantID);
            ViewData["RoleID"] = new SelectList(_context.Roles, "ID", "Description", merchantUser.RoleID);
            ViewData["UserID"] = new SelectList(_context.Users, "ID", "Email", merchantUser.UserID);
            return View(merchantUser);
        }

        // GET: MerchantUsers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var merchantUser = await _context.MerchantUsers
                .Include(m => m.Merchant)
                .Include(m => m.Role)
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (merchantUser == null)
            {
                return NotFound();
            }

            return View(merchantUser);
        }

        // POST: MerchantUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var merchantUser = await _context.MerchantUsers.FindAsync(id);
            _context.MerchantUsers.Remove(merchantUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MerchantUserExists(int id)
        {
            return _context.MerchantUsers.Any(e => e.ID == id);
        }
    }
}
