using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace rodcon.Controllers
{
    public class BaseController : Controller
    {
        public IActionResult Navbar()
        {
            return PartialView("_NavbarPartial");
        }
    }
}