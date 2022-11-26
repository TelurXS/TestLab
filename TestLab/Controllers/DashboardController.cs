using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using TestLab.DataBase;
using TestLab.Entities;
using TestLab.Entities.Pagination;
using TestLab.Models;
using TestLab.Utils.Validation;

namespace TestLab.Controllers
{
    public class DashboardController : Controller
    {
        public DashboardController()
        {
            AccountsCollection = new Accounts(new TestLabContext());
        }

        public Accounts AccountsCollection { get; set; }

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
        public IActionResult Posts()
        {
            if (IsAuthenticated && HavePermission(Config.Dashboard.RequiredPermission))
            {
                return View();
            }

            return View("DontHavePermission");
        }

        [HttpGet]
        public IActionResult Accounts([DefaultValue("")] string search, [DefaultValue(1)] int page, [DefaultValue(9)] int count)
        {
            if (IsAuthenticated && HavePermission(Config.Dashboard.RequiredPermission))
            {
                DashboardAccountsViewModel model = GetViewModel(search, count, page);

                return View(model);
            }

            return View("DontHavePermission");
        }

        [HttpPost]
        public IActionResult ChangeAccountInfo([DefaultValue("")] string search, [DefaultValue(1)] int page, [DefaultValue(9)] int count, 
            [DefaultValue(0)] int id, [DefaultValue("")] string name,
            [DefaultValue("")] string description, [DefaultValue("")] string phone, 
            [DefaultValue("")] string email, [DefaultValue("")] string address,
            DateTime birthDate, DateTime registrationDate, AccountState state)
        {
            Account account = AccountsCollection.GetOne(id);

            DashboardAccountsViewModel model = GetViewModel(search, count, page);

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

        private DashboardAccountsViewModel GetViewModel(string pattern, int count, int page)
        {
            PagenableCollection<Account> accounts;

            if (string.IsNullOrEmpty(pattern))
            {
                accounts = new PagenableCollection<Account>(AccountsCollection.GetAll(), count, page, "/dashboard/accounts");
            }
            else
            {
                accounts = new PagenableCollection<Account>(AccountsCollection.Search(pattern), count, page, "/dashboard/accounts");
            }

            DashboardAccountsViewModel model =
                new DashboardAccountsViewModel { Accounts = accounts, SearchPatter = pattern };

            return model;
        }
    }
}
