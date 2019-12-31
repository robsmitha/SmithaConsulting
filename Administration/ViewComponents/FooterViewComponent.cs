using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using DataLayer.Entities;
using DomainLayer.Enums;
using Administration.Constants;
using Administration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Administration.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var userId = HttpContext.Session?.GetInt32("userId");
            var username = HttpContext.Session?.GetString("username");
            var footer = new FooterViewModel(userId, username);
            return View("Footer", footer);
        }
    }
}
