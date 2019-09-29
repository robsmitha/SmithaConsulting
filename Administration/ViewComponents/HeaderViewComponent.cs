using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Architecture;
using Architecture.Data;
using Architecture.Enums;
using Administration.Constants;
using Administration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Administration.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var userId = HttpContext.Session?.GetInt32("userId");
            var username = HttpContext.Session?.GetString("username");
            var header = new HeaderViewModel(userId, username);
            return View("Header", header);
        }
    }
}
