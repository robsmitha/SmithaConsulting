using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataLayer;
using DataLayer.Data;
using DataLayer.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThemesController : ControllerBase
    {
        private readonly DbArchitecture _context;

        public ThemesController(DbArchitecture context)
        {
            _context = context;
        }

        // GET: api/Themes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ThemeModel>>> GetThemes()
        {
            var themes = _context.Themes;
            return await themes.Select(x => new ThemeModel(x)).ToListAsync();
        }

        // GET: api/Themes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ThemeModel>> GetTheme(int id)
        {
            var theme = await _context.Themes.FindAsync(id);

            if (theme == null)
            {
                return NotFound();
            }
            
            return new ThemeModel(theme);
        }

        // PUT: api/Themes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTheme(int id, Theme theme)
        {
            if (id != theme.ID)
            {
                return BadRequest();
            }

            _context.Entry(theme).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ThemeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Themes
        [HttpPost]
        public async Task<ActionResult<Theme>> PostTheme(Theme theme)
        {
            _context.Themes.Add(theme);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTheme", new { id = theme.ID }, theme);
        }

        // DELETE: api/Themes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Theme>> DeleteTheme(int id)
        {
            var theme = await _context.Themes.FindAsync(id);
            if (theme == null)
            {
                return NotFound();
            }

            _context.Themes.Remove(theme);
            await _context.SaveChangesAsync();

            return theme;
        }

        private bool ThemeExists(int id)
        {
            return _context.Themes.Any(e => e.ID == id);
        }
    }
}
