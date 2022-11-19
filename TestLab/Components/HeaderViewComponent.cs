using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TestLab.DataBase;
using TestLab.Entities;
using TestLab.Models;

namespace TestLab.Components
{
    public class HeaderViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            IEnumerable<INavigation> navigations
                = new Navigations(new TestLabContext()).AssignChildrens();

            HeaderViewModel model = new HeaderViewModel
            {
                Navigations = navigations,
                User = User.Identity,
            };

            return View("Header", model);
        }
    }
}
