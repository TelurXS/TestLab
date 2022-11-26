using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using TestLab.DataBase;
using TestLab.Entities;
using TestLab.Entities.Pagination;
using TestLab.Models;

namespace TestLab.Controllers
{
    public class NewsController : Controller
    {
        public NewsController()
        {
            TestLabContext context = new TestLabContext();

            Posts = new Posts(context);
            Accounts = new Accounts(context);
        }

        public Posts Posts { get; set; }
        public Accounts Accounts { get; set; }

        [HttpGet]
        public IActionResult Index([DefaultValue(1)] int page, [DefaultValue(9)] int count)
        {
            IEnumerable<Post> posts = Posts.GetAll();

            PagenableCollection<Post> collection =
                new PagenableCollection<Post>(posts, count, page, "/news");

            return View(collection);
        }

        [HttpGet]
        public IActionResult Post(int id) 
        {
            Post post = Posts.GetOne(id);

            if (post is not null)
            {
                Account author = Accounts.GetOne(post.AuthorId);

                PostViewModel model = new PostViewModel
                {
                    Post = post,
                    Author = author,
                };

                return View(model);
            }
            else 
            {
                return View("PostNotFound");
            }
        }
    }
}
