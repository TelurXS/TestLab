using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TestLab.Models;
using TestLab.Utils.Validation;

namespace TestLab.Controllers
{
    public class AccountController : Controller
    {
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
        public IActionResult Register(string name, string login, string email, string password, string confirmPassword, string termsOfUse) 
        {

            List<TextValidator> validators = new List<TextValidator>
            {
                new TextValidator(name, "Name").Max(64),
                new TextValidator(login, "Login").Max(32).Min(3),
                new TextValidator(email, "Email").Max(64),
                new TextValidator(password, "Password").Max(64),
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

            return View("SuccessfullyRegistration");
        }
    }
}
