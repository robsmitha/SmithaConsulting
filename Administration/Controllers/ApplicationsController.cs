using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Architecture;
using Architecture.Data;
using Microsoft.AspNetCore.Http;
using Administration.Constants;

namespace Administration.Controllers
{
    public class ApplicationsController : BaseController
    {
        private readonly DbArchitecture _context;

        public ApplicationsController(DbArchitecture context) : base(context)
        {
            _context = context;
        }

        // GET: Applications
        public async Task<IActionResult> Index()
        {
            var dbArchitecture = _context.Applications.Include(a => a.ApplicationType).Include(a => a.Theme);
            return View(await dbArchitecture.ToListAsync());
        }

        // GET: Applications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var application = await _context.Applications
                .Include(a => a.ApplicationType)
                .Include(a => a.Theme)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (application == null)
            {
                return NotFound();
            }

            return View(application);
        }

        // GET: Applications/Create
        public IActionResult Create()
        {
            ViewData["ApplicationTypeID"] = new SelectList(_context.ApplicationTypes, "ID", "ID");
            ViewData["ThemeID"] = new SelectList(_context.Themes, "ID", "ID");
            return View();
        }

        // POST: Applications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,ApplicationTypeID,ThemeID,ID,CreatedAt,Active,ModifiedTime")] Application application)
        {
            if (ModelState.IsValid)
            {
                _context.Add(application);
                await _context.SaveChangesAsync();
                if(application.ID == ApplicationID)
                {
                    var theme = await _context.Themes.SingleOrDefaultAsync(x => x.ID == application.ThemeID);
                    HttpContext.Session.SetString(SessionKeysConstants.THEME_CDN, theme?.StyleSheetCDN);
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApplicationTypeID"] = new SelectList(_context.ApplicationTypes, "ID", "ID", application.ApplicationTypeID);
            ViewData["ThemeID"] = new SelectList(_context.Themes, "ID", "ID", application.ThemeID);
            return View(application);
        }

        // GET: Applications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var application = await _context.Applications.FindAsync(id);
            if (application == null)
            {
                return NotFound();
            }
            ViewData["ApplicationTypeID"] = new SelectList(_context.ApplicationTypes, "ID", "ID", application.ApplicationTypeID);
            ViewData["ThemeID"] = new SelectList(_context.Themes, "ID", "ID", application.ThemeID);
            return View(application);
        }

        // POST: Applications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Description,ApplicationTypeID,ThemeID,ID,CreatedAt,Active,ModifiedTime")] Application application)
        {
            if (id != application.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(application);
                    await _context.SaveChangesAsync();
                    if (application.ID == ApplicationID)
                    {
                        var theme = await _context.Themes.SingleOrDefaultAsync(x => x.ID == application.ThemeID);
                        HttpContext.Session.SetString(SessionKeysConstants.THEME_CDN, theme?.StyleSheetCDN);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationExists(application.ID))
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
            ViewData["ApplicationTypeID"] = new SelectList(_context.ApplicationTypes, "ID", "ID", application.ApplicationTypeID);
            ViewData["ThemeID"] = new SelectList(_context.Themes, "ID", "ID", application.ThemeID);
            return View(application);
        }

        // GET: Applications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var application = await _context.Applications
                .Include(a => a.ApplicationType)
                .Include(a => a.Theme)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (application == null)
            {
                return NotFound();
            }

            return View(application);
        }

        // POST: Applications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var application = await _context.Applications.FindAsync(id);
            _context.Applications.Remove(application);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicationExists(int id)
        {
            return _context.Applications.Any(e => e.ID == id);
        }
    }
}
