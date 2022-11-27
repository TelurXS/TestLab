using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using TestLab.Utils.Files;

namespace TestLab.Controllers
{
    public class DebugController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult PostImage(IFormFile image) 
        {
            new FileParser().SaveUserImage(image, out string name);

            Console.WriteLine(name);

            return Redirect("/debug");
        }
    }
}
