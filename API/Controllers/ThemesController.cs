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
using DataLayer.DAL;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThemesController : ControllerBase
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ThemesController(DbArchitecture context, IMapper mapper)
        {
            _unitOfWork = new UnitOfWork(context);
            _mapper = mapper;
        }

        // GET: api/Themes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ThemeModel>>> GetThemes()
        {
            try
            {
                var themes = await _unitOfWork
                    .ThemeRepository
                    .GetAllAsync();
                return Ok(_mapper.Map<IEnumerable<ThemeModel>>(themes));
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
                var theme = await _unitOfWork
                    .ThemeRepository
                    .GetAsync(t => t.ID == id);

                if (theme == null)
                {
                    return NotFound();
                }

                return Ok(_mapper.Map<ThemeModel>(theme));
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
            var theme = await _unitOfWork
                    .ThemeRepository
                    .GetAsync(t => t.ID == id);

            if (theme == null)
            {
                return NotFound();
            }

            try
            {
                _mapper.Map(model, theme);
                _unitOfWork.ThemeRepository.Update(theme);
                await _unitOfWork.SaveAsync();
                return Ok(_mapper.Map<ThemeModel>(theme));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // POST: api/Themes
        [HttpPost]
        public async Task<ActionResult<ThemeModel>> PostTheme(ThemeModel model)
        {
            try
            {
                var theme = _mapper.Map<Theme>(model);
                _unitOfWork.ThemeRepository.Add(theme);
                await _unitOfWork.SaveAsync();
                return CreatedAtAction("GetTheme", new { id = theme.ID });
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
                _unitOfWork.ThemeRepository.Delete(id);
                await _unitOfWork.SaveAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
