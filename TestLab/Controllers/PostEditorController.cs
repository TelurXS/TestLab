using Microsoft.AspNetCore.Mvc;

namespace TestLab.Controllers
{
    public class PostEditorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
