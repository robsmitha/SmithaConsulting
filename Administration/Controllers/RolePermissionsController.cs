using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataLayer.Data;
using DomainLayer.Entities;

namespace Administration.Controllers
{
    public class RolePermissionsController : BaseController
    {
        private readonly OperationsContext _context;

        public RolePermissionsController(OperationsContext context) : base(context)
        {
            _context = context;
        }

        // GET: RolePermissions
        public async Task<IActionResult> Index()
        {
            var DbArchitecture = _context.RolePermissions.Include(r => r.Permission).Include(r => r.Role);
            return View(await DbArchitecture.ToListAsync());
        }

        // GET: RolePermissions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rolePermission = await _context.RolePermissions
                .Include(r => r.Permission)
                .Include(r => r.Role)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (rolePermission == null)
            {
                return NotFound();
            }

            return View(rolePermission);
        }

        // GET: RolePermissions/Create
        public IActionResult Create()
        {
            ViewData["PermissionID"] = new SelectList(_context.Permissions, "ID", "Description");
            ViewData["RoleID"] = new SelectList(_context.Roles, "ID", "Description");
            return View();
        }

        // POST: RolePermissions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoleID,PermissionID,ID,CreatedAt,Active,ModifiedTime")] RolePermission rolePermission)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rolePermission);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PermissionID"] = new SelectList(_context.Permissions, "ID", "Description", rolePermission.PermissionID);
            ViewData["RoleID"] = new SelectList(_context.Roles, "ID", "Description", rolePermission.RoleID);
            return View(rolePermission);
        }

        // GET: RolePermissions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rolePermission = await _context.RolePermissions.FindAsync(id);
            if (rolePermission == null)
            {
                return NotFound();
            }
            ViewData["PermissionID"] = new SelectList(_context.Permissions, "ID", "Description", rolePermission.PermissionID);
            ViewData["RoleID"] = new SelectList(_context.Roles, "ID", "Description", rolePermission.RoleID);
            return View(rolePermission);
        }

        // POST: RolePermissions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RoleID,PermissionID,ID,CreatedAt,Active,ModifiedTime")] RolePermission rolePermission)
        {
            if (id != rolePermission.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rolePermission);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RolePermissionExists(rolePermission.ID))
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
            ViewData["PermissionID"] = new SelectList(_context.Permissions, "ID", "Description", rolePermission.PermissionID);
            ViewData["RoleID"] = new SelectList(_context.Roles, "ID", "Description", rolePermission.RoleID);
            return View(rolePermission);
        }

        // GET: RolePermissions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rolePermission = await _context.RolePermissions
                .Include(r => r.Permission)
                .Include(r => r.Role)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (rolePermission == null)
            {
                return NotFound();
            }

            return View(rolePermission);
        }

        // POST: RolePermissions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rolePermission = await _context.RolePermissions.FindAsync(id);
            _context.RolePermissions.Remove(rolePermission);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RolePermissionExists(int id)
        {
            return _context.RolePermissions.Any(e => e.ID == id);
        }
    }
}
