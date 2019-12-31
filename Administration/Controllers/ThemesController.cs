using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataLayer.Entities;
using DataLayer.Data;
using Administration.Models;

namespace Administration.Controllers
{
    public class ThemesController : BaseController
    {
        private readonly DbArchitecture _context;

        public ThemesController(DbArchitecture context) : base(context)
        {
            _context = context;
        }

        // GET: Themes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Themes.ToListAsync());
        }

        // GET: Themes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var theme = await _context.Themes
                .FirstOrDefaultAsync(m => m.ID == id);
            if (theme == null)
            {
                return NotFound();
            }

            return View(theme);
        }

        // GET: Themes/Create
        public async Task<IActionResult> Create()
        {
            var model = new ThemeCreateViewModel();
            var themeList = await GetThemeList();
            ViewData["Themes"] = new SelectList(themeList.themes, "cssCdn", "name");
            return View(model);
        }

        // POST: Themes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,StyleSheetCDN")] ThemeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var theme = new Theme
                {
                    Name = model.Name,
                    StyleSheetCDN = model.StyleSheetCDN,
                    CreatedAt = DateTime.Now,
                    Active = true
                };
                _context.Add(theme);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var themeList = await GetThemeList();
            ViewData["Themes"] = new SelectList(themeList.themes, "cssCdn", "name");
            return View(model);
        }

        // GET: Themes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var theme = await _context.Themes.FindAsync(id);
            if (theme == null)
            {
                return NotFound();
            }
            return View(theme);
        }

        // POST: Themes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,StyleSheetCDN,ID,CreatedAt,Active,ModifiedTime")] Theme theme)
        {
            if (id != theme.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(theme);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ThemeExists(theme.ID))
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
            return View(theme);
        }

        // GET: Themes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var theme = await _context.Themes
                .FirstOrDefaultAsync(m => m.ID == id);
            if (theme == null)
            {
                return NotFound();
            }

            return View(theme);
        }

        // POST: Themes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var theme = await _context.Themes.FindAsync(id);
            _context.Themes.Remove(theme);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ThemeExists(int id)
        {
            return _context.Themes.Any(e => e.ID == id);
        }
    }
}
