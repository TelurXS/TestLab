using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TestLab.DataBase;
using TestLab.Entities;

namespace TestLab.Components
{
    public class HeaderViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            IEnumerable<INavigation> navigations
                = new Navigations(new TestLabContext()).AssignChildrens();

            return View("Header", navigations);
        }
    }
}
