using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using TestLab.DataBase;
using TestLab.Entities;

namespace TestLab.Controllers
{
    public class DebugController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
