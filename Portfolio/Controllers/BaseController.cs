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
using DataModeling;
using DataModeling.Data;
using Architecture.Enums;
using Portfolio.Models;
using Portfolio.Utilities;
using Portfolio.Constants;

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
        public string ThemeCDN => HttpContext.Session.GetString(SessionKeysConstants.THEME_CDN);
        public int? ApplicationID = int.TryParse(ConfigurationManager.GetConfiguration("ApplicationID"), out var @int) ? (int?)@int : null;
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                //TODO: Handle session timeout
                return;
            }

            #region Set Theme in Session
            if (ThemeCDN == null && ApplicationID > 0)
            {
                var application = _context.Applications.SingleOrDefault(x => x.ID == ApplicationID);
                if (application != null)
                {
                    var theme = _context.Themes.SingleOrDefault(t => t.ID == application.ThemeID);
                    if (theme != null)
                    {
                        //Set theme
                        HttpContext.Session.SetString(SessionKeysConstants.THEME_CDN, theme?.StyleSheetCDN);
                    }
                }
            }
            #endregion

        }
    }
}