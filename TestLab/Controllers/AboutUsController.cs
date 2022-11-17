using Microsoft.AspNetCore.Mvc;

namespace TestLab.Controllers
{
    public class AboutUsController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Contacts()
        {
            return View();
        }

        [HttpGet]
        public IActionResult TermsOfUse() 
        {
            return View();
        }
    }
}
