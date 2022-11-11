using Microsoft.AspNetCore.Mvc;

namespace TestLab.Components
{
    public class HeadViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("Head");
        }
    }
}
