using Microsoft.AspNetCore.Mvc;

namespace TestLab.Components
{
    public class FooterViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("Footer");
        }
    }
}
