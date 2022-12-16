using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using TestLab.DataBase;
using TestLab.Entities;
using TestLab.Entities.Pagination;
using TestLab.Entities.Projects;
using TestLab.Models;
using TestLab.Utils.Files;
using TestLab.Utils.Validation;

namespace TestLab.Controllers
{
    public class DashboardController : Controller
    {
        public DashboardController()
        {
            TestLabContext context = new TestLabContext();

            AccountsCollection = new Accounts(context);
            PostsCollection = new Posts(context);
            ProjectsCollection = new Projects(context);
            Parser = new FileParser();
        }

        public Accounts AccountsCollection { get; set; }
        public Posts PostsCollection { get; set; }
        public Projects ProjectsCollection { get; set; }
        public FileParser Parser { get; set; }

        public bool IsAuthenticated => User.Identity.IsAuthenticated;
        public bool HavePermission(AccountState permission) => AccountsCollection.GetBySession(User).HavePermission(permission);

        [HttpGet]
        public IActionResult Index()
        {
            if (IsAuthenticated && HavePermission(Config.Dashboard.RequiredPermission))
            {
                return View();
            }

            return View("DontHavePermission");
        }

        [HttpGet]
        public IActionResult Posts([DefaultValue("")] string search, [DefaultValue(1)] int page, [DefaultValue(9)] int count)
        {
            if (IsAuthenticated && HavePermission(Config.Dashboard.RequiredPermission))
            {
                DashboardPostsViewModel model = GetPostsViewModel(search, count, page);

                return View(model);
            }

            return View("DontHavePermission");
        }

        [HttpGet]
        public IActionResult Accounts([DefaultValue("")] string search, [DefaultValue(1)] int page, [DefaultValue(9)] int count)
        {
            if (IsAuthenticated && HavePermission(Config.Dashboard.RequiredPermission))
            {
                DashboardAccountsViewModel model = GetAccountsViewModel(search, count, page);

                return View(model);
            }

            return View("DontHavePermission");
        }

        [HttpGet]
        public IActionResult Projects([DefaultValue("")] string search, [DefaultValue(1)] int page, [DefaultValue(9)] int count)
        {
            if (IsAuthenticated && HavePermission(Config.Dashboard.RequiredPermission))
            {
                DashboardProjectsViewModel model = GetProjectsViewModel(search, count, page);

                return View(model);
            }

            return View("DontHavePermission");
        }

        [HttpPost]
        public IActionResult ChangeAccountInfo([DefaultValue("")] string search, [DefaultValue(1)] int page, [DefaultValue(9)] int count, 
            [DefaultValue(0)] int id, [DefaultValue("")] string name, [DefaultValue("")] string description, 
            [DefaultValue("")] string phone, [DefaultValue("")] string email, [DefaultValue("")] string address,
            IFormFile profileImage, DateTime birthDate, DateTime registrationDate, AccountState state)
        {
            Account account = AccountsCollection.GetOne(id);

            DashboardAccountsViewModel model = GetAccountsViewModel(search, count, page);

            if (account is not null)
            {
                List<TextValidator> validators = new List<TextValidator>()
                {
                    new TextValidator(name, "Name").Min(3).Max(64),
                    new TextValidator(description, "Description").Max(128),
                    new TextValidator(email, "Email").Max(64),
                    new TextValidator(phone, "Phone").Max(32),
                    new TextValidator(address, "Address").Max(64),
                };

                foreach (TextValidator validator in validators)
                {
                    if (validator.IsInvalid)
                    {
                        model.Message = validator.Message;
                        return View(nameof(Accounts), model);
                    }
                }

                if (profileImage is not null)
                {
                    bool saveResult = Parser.ReplaceUserImage(profileImage, account.ProfileImage, out string fileName);

                    if (saveResult is true)
                    {
                        account.ProfileImage = fileName;
                    }
                    else 
                    {
                        model.Message = "Image is not valid";
                        return View(nameof(Accounts), model);
                    }
                }

                account.Name = name;
                account.Description = description;
                account.Phone = phone;
                account.Email = email;
                account.Address = address;
                account.BirthDate = birthDate;
                account.RegistrationDate = registrationDate;
                account.State = state;

                if (AccountsCollection.Save())
                {
                    model.Message = "Successfully saved";
                    model.MessageType = MessageType.Success;
                    return View(nameof(Accounts), model);
                }
            }

            model.Message = "Something wrong";
            model.MessageType = MessageType.Danger;
            return View(nameof(Accounts), model);
        }

        [HttpPost]
        public IActionResult ChangePostInfo([DefaultValue("")] string search, [DefaultValue(1)] int page, [DefaultValue(9)] int count,
            [DefaultValue(0)] int id, [DefaultValue("")] string title, [DefaultValue("")] string description,
            [DefaultValue("")] string content, DateTime creationDate, DateTime releaseDate, IFormFile postImage, PostState state)
        {
            Post post = PostsCollection.GetOne(id);

            DashboardPostsViewModel model = GetPostsViewModel(search, count, page);

            if (post is not null) 
            {
                List<TextValidator> validators = new List<TextValidator>()
                {
                    new TextValidator(title, "Title").Min(3).Max(64),
                    new TextValidator(description, "Description").Max(128),
                    new TextValidator(content, "Content").Max(512),
                };

                foreach (TextValidator validator in validators)
                {
                    if (validator.IsInvalid)
                    {
                        model.Message = validator.Message;
                        return View(nameof(Posts), model);
                    }
                }

                if (postImage is not null)
                {
                    bool saveResult = Parser.ReplacePostImage(postImage, post.Image, out string fileName);

                    if (saveResult is true)
                    {
                        post.Image = fileName;
                    }
                    else
                    {
                        model.Message = "Image is not valid";
                        return View(nameof(Posts), model);
                    }
                }

                post.Title = title;
                post.Description = description;
                post.Content = content;
                post.CreationDate = creationDate;
                post.ReleaseDate = releaseDate;
                post.State = state;

                if (PostsCollection.Save())
                {
                    model.Message = "Successfully saved";
                    model.MessageType = MessageType.Success;
                    return View(nameof(Posts), model);
                }
            }

            model.Message = "Something wrong";
            model.MessageType = MessageType.Danger;
            return View(nameof(Posts), model);
        }

        [HttpPost]
        public IActionResult ReleasePost([DefaultValue(0)] int id) 
        {
            Post post = PostsCollection.GetOne(id);
            Console.WriteLine(id);

            if (post is not null)
            {
                post.State = PostState.Published;
                post.ReleaseDate = DateTime.Now;

                PostsCollection.Save();
            }

            return Redirect("/dashboard/posts");
        }

        private DashboardAccountsViewModel GetAccountsViewModel(string search, int count, int page)
        {
            PagenableCollection<Account> accounts;

            if (string.IsNullOrEmpty(search))
            {
                accounts = new PagenableCollection<Account>(AccountsCollection.GetAll(), count, page, $"/dashboard/accounts?search={search}");
            }
            else
            {
                accounts = new PagenableCollection<Account>(AccountsCollection.Search(search), count, page, $"/dashboard/accounts?search={search}");
            }

            DashboardAccountsViewModel model =
                new DashboardAccountsViewModel { Accounts = accounts, SearchPattern = search };

            return model;
        }

        private DashboardPostsViewModel GetPostsViewModel(string search, int count, int page)
        {
            PagenableCollection<Post> posts;

            if (string.IsNullOrEmpty(search))
            {
                posts = new PagenableCollection<Post>(PostsCollection.GetAll(), count, page, $"/dashboard/accounts?search={search}");
            }
            else
            {
                posts = new PagenableCollection<Post>(PostsCollection.Search(search), count, page, $"/dashboard/accounts?search={search}");
            }

            DashboardPostsViewModel model =
                new DashboardPostsViewModel { Posts = posts, SearchPattern = search };

            return model;
        }

        private DashboardProjectsViewModel GetProjectsViewModel(string search, int count, int page)
        {
            PagenableCollection<Project> projects;

            if (string.IsNullOrEmpty(search))
            {
                projects = new PagenableCollection<Project>(ProjectsCollection.GetAll(), count, page, $"/dashboard/projects?search={search}");
            }
            else
            {
                projects = new PagenableCollection<Project>(ProjectsCollection.Search(search), count, page, $"/dashboard/projects?search={search}");
            }

            DashboardProjectsViewModel model =
                new DashboardProjectsViewModel { Projects = projects, SearchPattern = search };

            return model;
        }
    }
}
