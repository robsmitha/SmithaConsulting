using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataLayer;
using DataLayer.Data;
using Administration.Models;
using DomainLayer.Utilities;

namespace Administration.Controllers
{
    public class UsersController : BaseController
    {
        private readonly DbArchitecture _context;

        public UsersController(DbArchitecture context) : base(context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index(int? merchantId)
        {
            merchantId = merchantId ?? MerchantID;
            var merchantUsers = _context.MerchantUsers.Where(x => x.MerchantID == merchantId).Include(o => o.User);
            return View(await merchantUsers.Select(x => x.User).ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.ID == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            var model = new UserCreateViewModel();
            return View(model);
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                if(await _context.Users.SingleOrDefaultAsync(x => x.Username == model.Username) == null)
                {

                    var user = new User();
                    user.FirstName = model.FirstName;
                    user.MiddleName = model.MiddleName;
                    user.LastName = model.LastName;
                    user.Email = model.Email;
                    user.Active = model.Active;
                    user.CreatedAt = DateTime.Now;

                    //TODO: implement pw reset email
                    user.Password = SecurePasswordHasher.Hash("password");
                    _context.Add(user);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("CustomError", $"The username {model.Username} is already taken");
                }
            }
            return View(model);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var model = new UserEditViewModel(user);
            return View(model);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users.SingleOrDefaultAsync(x => x.ID == model.UserID);

                if (user == null)
                {
                    return NotFound();
                }
                if(model.Username != user.Username && await _context.Users.SingleOrDefaultAsync(x => x.Username == model.Username) != null)
                {
                    //username is not available
                    ModelState.AddModelError("CustomError", $"The username {model.Username} is already taken");
                }
                else
                {
                    user.FirstName = model.FirstName;
                    user.MiddleName = model.MiddleName;
                    user.LastName = model.LastName;
                    user.Email = model.Email;
                    user.Username = model.Username;
                    user.Active = model.Active;
                    user.ModifiedTime = DateTime.Now;
                    try
                    {
                        _context.Update(user);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!UserExists(user.ID))
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

            }

            return View(model);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.ID == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.ID == id);
        }
    }
}
