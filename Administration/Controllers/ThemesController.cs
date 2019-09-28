using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Architecture.Data;
using Administration.Constants;
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
        public async Task<IActionResult> Index()
        {
            var model = await GetThemeList();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Apply(string cssCdn)
        {
            if (ModelState.IsValid)
            {
                //set theme in session
                HttpContext.Session.SetString(SessionKeysConstants.THEME_CDN, cssCdn);
            }

            return RedirectToAction("Index");
        }
    }
}