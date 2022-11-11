using Microsoft.AspNetCore.Mvc;

namespace TestLab.Components
{
    public class ScriptsViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("Scripts");
        }
    }
}
