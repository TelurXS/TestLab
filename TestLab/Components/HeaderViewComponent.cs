using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TestLab.DataBase;
using TestLab.Entities;
using TestLab.Models;

namespace TestLab.Components
{
    public class HeaderViewComponent : ViewComponent
    {
        public HeaderViewComponent()
        {
            TestLabContext context = new TestLabContext();

            Navigations = new Navigations(context);
            Accounts = new Accounts(context);
        }

        public Navigations Navigations { get; set; }
        public Accounts Accounts { get; set; }

        public IViewComponentResult Invoke()
        {
            IEnumerable<INavigation> navigations = Navigations.AssignChildrens();
            Account account = Accounts.GetBySession(User);

            HeaderViewModel model = new HeaderViewModel
            {
                Navigations = navigations,
                User = User.Identity,
                Account = account
            };

            return View("Header", model);
        }
    }
}
