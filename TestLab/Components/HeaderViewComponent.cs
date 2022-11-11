using Microsoft.AspNetCore.Mvc;

namespace TestLab.Components
{
    public class HeaderViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("Header");
        }
    }
}
