using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TestLab.DataBase;
using TestLab.Entities;
using TestLab.Models;
using TestLab.Utils;
using TestLab.Utils.User;
using TestLab.Utils.Validation;

namespace TestLab.Controllers
{
    public class AccountController : Controller
    {
        public Accounts Accounts { get; } = new Accounts(new TestLabContext());


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
                    .Execute(new Hasher().Verify(password, account?.Password)).ErrorMessage("Wrong password"),
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
        public IActionResult SignOut() 
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

            Account account = Account.Create(name, login, password);

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
            if (User.Identity.IsAuthenticated)
            {
                Account account = Accounts.GetOneByLogin(User.Identity.Name);

                if (account is null) 
                {
                    new Session(HttpContext).Destroy();
                    return Redirect("/account/login");
                }

                return View(account);
            }
            return Redirect("/account/login");
        }

        [HttpGet]
        public IActionResult Profile(int id) 
        {
            Account account = Accounts.GetOne(id);

            if (account is not null)
            {
                return View(account);
            }
            else
            {
                return View("AccountNotFound");
            }
        }
    }
}
