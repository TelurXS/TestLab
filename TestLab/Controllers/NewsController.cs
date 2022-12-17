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
            PostLikes = new PostLikes(context);
        }

        public Posts Posts { get; set; }
        public Accounts Accounts { get; set; }
        public PostLikes PostLikes { get; set; }

        [HttpGet]
        public IActionResult Index([DefaultValue(1)] int page, [DefaultValue(9)] int count)
        {
            IEnumerable<Post> posts = Posts.GetLatestAvailable();

            PagenableCollection<Post> collection =
                new PagenableCollection<Post>(posts, count, page, "/news");

            return View(collection);
        }

        [HttpGet]
        public IActionResult Post(int id)
        {
            Account account = Accounts.GetBySession(User);
            Post post = Posts.GetOne(id);

            if (post is not null)
            {
                Account author = Accounts.GetOne(post.AuthorId);

                PostViewModel model = new PostViewModel
                {
                    Post = post,
                    Author = author,
                    IsLiked = account is not null ? PostLikes.IsAccountLikePost(account, post) : false,
                    LikeCount = PostLikes.LikeCount(post),
                };

                return View(model);
            }

            return View("PostNotFound");
        }

        [HttpPost]
        public IActionResult ToggleLike(int id)
        {
            Account account = Accounts.GetBySession(User);
            Post post = Posts.GetOne(id);

            if (account is null)
                return Redirect("/account/login");

            if (post is null)
                return View("PostNotFound");

            PostLikes.ToogleLike(account, post);

            return Redirect($"/news/post?id={id}");
        }
    }
}
