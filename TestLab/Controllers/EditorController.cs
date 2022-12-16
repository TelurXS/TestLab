using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel;
using TestLab.DataBase;
using TestLab.Entities;
using TestLab.Models;
using TestLab.Utils.Files;

namespace TestLab.Controllers
{
    public class EditorController : Controller
    {
        public EditorController()
        {
            TestLabContext context = new TestLabContext();

            Accounts = new Accounts(context);
            Posts = new Posts(context);
            Parser = new FileParser();
        }

        public Accounts Accounts { get; set; }
        public Posts Posts { get; set; }
        public FileParser Parser { get; set; }

        [HttpGet]
        public IActionResult Index([DefaultValue(0)] int id)
        {
            Account account = Accounts.GetBySession(User);

            if (account is null)
                return Redirect("/account/login");

            if (account.HavePermission(Config.Editor.EnterPermission) is false)
                return View("DontHavePermission");

            Post post = Posts.GetOne(id);
            Account author = Accounts.GetOne(post?.Id ?? 0);

            EditorViewModel model = new EditorViewModel
            {
                Post = post,
                Author = author,
                IsViewAllowed = account == author || account.HavePermission(Config.Editor.EditPermission),
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Create([DefaultValue("")] string title, [DefaultValue("")] string description,
            [DefaultValue("")] string content, IFormFile image)
        {
            Account account = Accounts.GetBySession(User);

            if (account is null)
                return Redirect("/account/login");

            Post post = Post.Create(title, description, content, account.Id);

            if(image is not null) 
            {
                if (Parser.ReplacePostImage(image, "", out string fileName))
                    post.Image = fileName;
            }

            if (Posts.Insert(post) is false)
                return BadRequest("Something wrong");

            return Json(post);
        }

        [HttpPost]
        public IActionResult Save([DefaultValue(0)] int id,
            [DefaultValue("")] string title, [DefaultValue("")] string description,
            [DefaultValue("")] string content, IFormFile image)
        {
            Account account = Accounts.GetBySession(User);

            if (account is null)
                return Redirect("/account/login");

            Post post = Posts.GetOne(id);

            if (post is null)
                return Create(title, description, content, image);

            post.Title = title;
            post.Description = description;
            post.Content = content; ;

            if (image is not null)
            {
                if(Parser.ReplacePostImage(image, post.Image, out string fileName))
                    post.Image = fileName;
            }

            if (Posts.Save() is false)
                return BadRequest("Something wrong");

            return Json(post);
        }
    }
}
