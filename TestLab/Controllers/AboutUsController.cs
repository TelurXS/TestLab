using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using TestLab.DataBase;
using TestLab.Entities;

namespace TestLab.Controllers
{
    public class AboutUsController : Controller
    {
        public AboutUsController()
        {
            TestLabContext context = new TestLabContext();

            Workers = new Workers(context);
        }

        public Workers Workers { get; set; }

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
            IEnumerable<Worker> workers = Workers.GetAll();

            return View(workers);
        }

        [HttpGet]
        public IActionResult TermsOfUse() 
        {
            return View();
        }
    }
}
