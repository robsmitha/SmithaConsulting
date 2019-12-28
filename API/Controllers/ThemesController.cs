using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataLayer;
using DataLayer.Data;
using DomainLayer.Models;
using AutoMapper;
using DataLayer.Repositories;
using DomainLayer.Services;
using DomainLayer.BLL;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThemesController : ControllerBase
    {
        private readonly BusinessLogic BLL;
        public ThemesController(DbArchitecture context, IMapper mapper)
        {
            if (BLL == null)
            {
                BLL = new BusinessLogic(context, mapper);
            }
        }

        // GET: api/Themes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ThemeModel>>> GetThemes()
        {
            try
            {
                var themes = await BLL.Themes.GetAllAsync();
                return Ok(themes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        // GET: api/Themes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ThemeModel>> GetTheme(int id)
        {
            try
            {
                var theme = await BLL.Themes.GetAsync(id);

                if (theme == null)
                {
                    return NotFound();
                }

                return Ok(theme);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        // PUT: api/Themes/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ThemeModel>> PutTheme(int id, ThemeModel model)
        {
            if (id != model.ID)
            {
                return BadRequest();
            }
            try
            {
                var theme = await BLL.Themes.UpdateAsync(model);
                if (theme == null)
                {
                    return NotFound();
                }
                return Ok(theme);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        // POST: api/Themes
        [HttpPost]
        public async Task<ActionResult<ThemeModel>> PostTheme(ThemeModel model)
        {
            try
            {
                return Ok(await BLL.Themes.AddAsync(model));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        // DELETE: api/Themes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTheme(int id)
        {
            try
            {
                await BLL.Applications.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
