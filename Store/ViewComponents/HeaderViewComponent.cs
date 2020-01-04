using Store.Models;
using Microsoft.AspNetCore.Mvc;
using Store.Constants;
using Microsoft.AspNetCore.Http;

namespace Store.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var userId = HttpContext.Session?.GetInt32(SessionKeysConstants.USER_ID);
            var merchantId = HttpContext.Session?.GetInt32(SessionKeysConstants.MERCHANT_ID);
            var username = HttpContext.Session?.GetString(SessionKeysConstants.USERNAME);
            var imageUrl = HttpContext.Session?.GetString(SessionKeysConstants.IMAGE_URL);
            var header = new HeaderViewModel(userId, merchantId, username, imageUrl);
            return View("Header", header);
        }
    }
}
