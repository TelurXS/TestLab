using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using TestLab.DataBase;
using TestLab.Entities;
using TestLab.Entities.Pagination;

namespace TestLab.Controllers
{
    public class NewsController : Controller
    {
        public NewsController()
        {
            Posts = new Posts(new TestLabContext());
        }

        public Posts Posts { get; set; }

        [HttpGet]
        public IActionResult Index([DefaultValue(1)] int page, [DefaultValue(9)] int count)
        {
            IEnumerable<Post> posts = Posts.GetAll();

            PagenableCollection<Post> collection =
                new PagenableCollection<Post>(posts, count, page);

            return View(collection);
        }

        [HttpGet]
        public IActionResult Post(int id) 
        {
            Post post = Posts.GetOne(id);

            //Validation

            return View(post);
        }
    }
}
