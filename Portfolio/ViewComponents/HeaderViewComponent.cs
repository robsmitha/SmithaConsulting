using Portfolio.Models;
using Microsoft.AspNetCore.Mvc;

namespace Portfolio.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("Header", new HeaderViewModel());
        }
    }
}
