using Store.Models;
using Microsoft.AspNetCore.Mvc;

namespace Store.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("Header", new HeaderViewModel());
        }
    }
}
