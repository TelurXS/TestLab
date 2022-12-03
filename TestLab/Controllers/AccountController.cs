using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using TestLab.DataBase;
using TestLab.Entities;
using TestLab.Models;
using TestLab.Utils;
using TestLab.Utils.Files;
using TestLab.Utils.Hashing;
using TestLab.Utils.User;
using TestLab.Utils.Validation;

namespace TestLab.Controllers
{
    public class AccountController : Controller
    {
        public AccountController()
        {
            Accounts = new Accounts(new TestLabContext());
            Projects = new Projects(new TestLabContext());
            Hasher = new Hasher();
            Parser = new FileParser();
        }

        public Accounts Accounts { get; set; }
        public Projects Projects { get; set; }
        public IHasher Hasher { get; set; }
        public FileParser Parser { get; set; }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string login, string password)
        {
            Account account = null;

            List<TextValidator> validators = new List<TextValidator>
            {
                new TextValidator(login, "Login").Min(1).ErrorMessage("Login cannot be empty").Max(64)
                    .Execute(Accounts.IsExsist(login, out account)).ErrorMessage("Account is not exsist"),
                new TextValidator(password, "Password").Min(1).ErrorMessage("Password cannot be empty").Max(64)
                    .Execute(Hasher.Verify(password, account?.Password)).ErrorMessage("Wrong password"),
            };

            FormViewModel model = new FormViewModel { };

            foreach (TextValidator validator in validators)
            {
                if (validator.IsInvalid)
                {
                    model.Message = validator.Message;
                    return View(nameof(Login), model);
                }
            }

            new Session(HttpContext).Create(account);

            return Redirect("/account/successfullylogin");
        }

        [HttpGet]
        public IActionResult SuccessfullyLogin()
        {
            return View("SuccessfullyLogin");
        }

        [HttpGet]
        public new IActionResult SignOut()
        {
            new Session(HttpContext).Destroy();

            return Redirect("/home");
        }

        [HttpPost]
        public IActionResult Register(string name, string login, string email, string password, string confirmPassword, string termsOfUse)
        {
            List<TextValidator> validators = new List<TextValidator>
            {
                new TextValidator(name, "Name").Min(1).Max(64),
                new TextValidator(login, "Login").Execute(Accounts.IsNotExsist(login)).ErrorMessage("This Login is already exsist").Max(32).Min(3),
                new TextValidator(email, "Email").Min(1).Max(64),
                new TextValidator(password, "Password").Min(1).Max(64),
                new TextValidator(confirmPassword, "Confirm Password").Match(password),
                new TextValidator(termsOfUse, "Terms of Use").Match("true").ErrorMessage("Please agree with terms of use"),
            };

            FormViewModel model = new FormViewModel { Form = Request.Form };

            foreach (TextValidator validator in validators)
            {
                if (validator.IsInvalid)
                {
                    model.Message = validator.Message;
                    return View(nameof(Registration), model);
                }
            }

            Account account = Account.Create(name, login, email, Hasher.Hash(password));

            if (Accounts.Insert(account))
            {
                return View("SuccessfullyRegistration");
            }

            model.Message = "Something wrong";
            return View(nameof(Registration), model);
        }

        [HttpGet]
        public IActionResult MyProfile()
        {
            if (User.Identity.IsAuthenticated is false)
                return Redirect("/account/login");

            Account account = Accounts.GetBySession(User);

            if (account is null)
            {
                new Session(HttpContext).Destroy();
                return Redirect("/account/login");
            }

            return View(new ProfileViewModel { Account = account, Projects = Projects.GetByAuthor(account.Id) });
        }

        [HttpGet]
        public IActionResult Profile(int id)
        {
            Account account = Accounts.GetOne(id);

            if (account is not null)
            {
                return View(new ProfileViewModel { Account = account, Projects = Projects.GetByAuthor(account.Id) });
            }
            else
            {
                return View("AccountNotFound");
            }
        }

        [HttpPost]
        public IActionResult ChangeAccountInfo([DefaultValue("")] string name, [DefaultValue("")] string description,
                                                [DefaultValue("")] string email, [DefaultValue("")] string phone,
                                                [DefaultValue("")] string address, DateTime birthDate)
        {
            if (User.Identity.IsAuthenticated is false)
                return Redirect("/account/login");

            Account account = Accounts.GetBySession(User);

            if (account is null)
            {
                new Session(HttpContext).Destroy();
                return Redirect("/account/login");
            }

            List<TextValidator> validators = new List<TextValidator>()
            {
                new TextValidator(name, "Name").Min(3).Max(64),
                new TextValidator(description, "Description").Max(128),
                new TextValidator(email, "Email").Max(64),
                new TextValidator(phone, "Phone").Max(32),
                new TextValidator(address, "Address").Max(64),
            };

            ProfileViewModel model = new ProfileViewModel { Account = account };

            foreach (TextValidator validator in validators)
            {
                if (validator.IsInvalid)
                {
                    model.Message = validator.Message;
                    return View(nameof(MyProfile), model);
                }
            }

            account.Name = name;
            account.Description = description;
            account.Email = email;
            account.Phone = phone;
            account.Address = address;
            account.BirthDate = birthDate;

            if (Accounts.Save() is false)
            {
                model.Message = "Something wrong";
                model.MessageType = MessageType.Danger;
                return View(nameof(MyProfile), model);
            }

            return Redirect("/account/myprofile");
        }

        [HttpPost]
        public IActionResult ChangeImage(IFormFile image)
        {
            if (User.Identity.IsAuthenticated is false)
                return Redirect("/account/login");

            Account account = Accounts.GetBySession(User);

            Parser.ReplaceUserImage(image, account.ProfileImage, out string name);

            account.ProfileImage = name;

            Accounts.Save();

            return Redirect("/account/myprofile");
        }
    }
}
