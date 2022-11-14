using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TestLab.Models;

namespace TestLab.Components
{
    public class PaginationCountSelectorViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(PaginationViewModel model)
        {
            return View("PaginationCountSelector", model);
        }
    }
}
