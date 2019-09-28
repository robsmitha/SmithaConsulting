using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Architecture;
using Architecture.Data;
using Architecture.Enums;
using Portfolio.Models;
using Portfolio.Utilities;

namespace Portfolio.Controllers
{
    public class BaseController : Controller
    {
        private readonly DbArchitecture _context;

        public BaseController(DbArchitecture context)
        {
            _context = context;
        }
        public string CDNLocation => ConfigurationManager.GetConfiguration("AWSCDN");
        public string BucketName => ConfigurationManager.GetConfiguration("S3BucketName");
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                //TODO: Handle session timeout
                return;
            }

            var actionName = ControllerContext.RouteData.Values["action"].ToString().ToLower();
            var controllerName = ControllerContext.RouteData.Values["controller"].ToString().ToLower();
            string[] publicPages = {
                "index", "login", "loginasync", "signout", "signup", "signupasync", "about",
                "privacy", "contact",
                //"payment", "details", "apply",
                "error"
            };
            switch (controllerName)
            {
                case "home":
                case "blog":
                //case "register":
                //case "orders":
                //case "theme":
                //case "chat":
                    if (Array.IndexOf(publicPages, actionName) != -1) return;
                    break;
            }
            context.Result = new RedirectResult("/Home/Login");
            return;

        }
    }
}