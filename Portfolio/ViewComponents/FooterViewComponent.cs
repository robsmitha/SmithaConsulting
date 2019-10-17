using Portfolio.Models;
using Microsoft.AspNetCore.Mvc;

namespace Portfolio.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("Footer", new FooterViewModel());
        }
    }
}
