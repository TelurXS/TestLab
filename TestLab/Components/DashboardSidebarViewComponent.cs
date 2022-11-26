using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using TestLab.Entities;
using TestLab.Models;
using Navigation = TestLab.Entities.Navigation;

namespace TestLab.Components
{
    public class DashboardSidebarViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string openedTab)
        {
            IEnumerable<Navigation> navigations = Config.Dashboard.Navigations;

            DashboardSidebarViewModel model = new DashboardSidebarViewModel
            {
                Navigations = navigations,
                OpenedTab = openedTab,
            };

            return View("DashboardSidebar", model);
        }
    }
}
