using Microsoft.AspNetCore.Mvc;
using TestLab.Entities;
using TestLab.Entities.Pagination;
using TestLab.Models;

namespace TestLab.Components
{
    public class PaginationViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(PaginationViewModel model)
        {
            return View("Pagination", model);
        }
    }
}
