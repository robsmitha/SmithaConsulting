using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Portfolio.Utilities;
using Portfolio.Constants;
using DomainLayer.Utilities;
using DomainLayer.Models;

namespace Portfolio.Controllers
{
    public class BaseController : Controller
    {
        public string ApplicationName => ConfigurationManager.GetConfiguration("ApplicationName");

        #region API
        public static string APIEndpoint = ConfigurationManager.GetConfiguration("APIEndpoint");
        public static string APIKey = "key";// ConfigurationManager.GetConfiguration("APIKey");
        public APIExtensions API = new APIExtensions(APIEndpoint, APIKey);
        #endregion

        public string CDNLocation => ConfigurationManager.GetConfiguration("AWSCDN");
        public string BucketName => ConfigurationManager.GetConfiguration("S3BucketName");
        public string ThemeCDN => HttpContext.Session.GetString(SessionKeysConstants.THEME_CDN);

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                //TODO: Handle session timeout
                return;
            }

            #region Set Theme in Session
            if (ThemeCDN == null && !string.IsNullOrEmpty(ApplicationName))
            {
                var application = API.Get<ApplicationModel>($"/applications/GetByName/{ApplicationName}");
                if (application != null)
                {
                    var theme = API.Get<ThemeModel>($"/themes/{application.ThemeID}");
                    if (theme != null)
                    {
                        HttpContext.Session.SetString(SessionKeysConstants.THEME_CDN, theme?.StyleSheetCDN);
                    }
                }
            }
            #endregion

        }
    }
}