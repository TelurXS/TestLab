using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TestLab.DataBase;
using TestLab.Entities;
using TestLab.Entities.Pagination;

namespace TestLab.Controllers
{
    public class NewsController : Controller
    {
        [HttpGet]
        public IActionResult Index(int? page, int? count)
        {
            IEnumerable<Post> posts = new Posts(new TestLabContext()).GetAll();

            PagenableCollection<Post> collection =
                new PagenableCollection<Post>(posts, count ?? 9, page ?? 1);

            return View(collection);
        }

        [HttpGet]
        public IActionResult Post(int id) 
        {
            Post post = new Posts(new TestLabContext()).GetOne(id);

            //Validation

            return View(post);
        }
    }
}
