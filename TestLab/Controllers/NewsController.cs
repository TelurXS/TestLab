﻿using Microsoft.AspNetCore.Mvc;

namespace TestLab.Controllers
{
    public class NewsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
