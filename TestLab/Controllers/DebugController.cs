using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using TestLab.DataBase;
using TestLab.Utils.Files;

namespace TestLab.Controllers
{
    public class DebugController : Controller
    {
        public IActionResult Index()
        {
            Projects projects = new Projects(new TestLabContext());

            return View(projects);
        }
    }
}
