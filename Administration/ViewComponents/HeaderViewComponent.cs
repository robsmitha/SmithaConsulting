using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Administration.Constants;
using Administration.Models;

namespace Administration.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var userId = HttpContext.Session?.GetInt32(SessionKeysConstants.USER_ID);
            var merchantId = HttpContext.Session?.GetInt32(SessionKeysConstants.MERCHANT_ID);
            var username = HttpContext.Session?.GetString(SessionKeysConstants.USERNAME);
            var header = new HeaderViewModel(userId, merchantId, username);
            return View("Header", header);
        }
    }
}
