using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DomainLayer.Services;
using Microsoft.AspNetCore.Mvc;

namespace Store.Controllers
{
    public class StoreController : BaseController
    {
        public StoreController(IApiService api, IMapper mapper, ICacheService cache) : base(api, mapper, cache) { }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Item()
        {
            return View();
        }
    }
}