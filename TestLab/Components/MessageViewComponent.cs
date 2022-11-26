using Microsoft.AspNetCore.Mvc;
using TestLab.Models;

namespace TestLab.Components
{
    public class MessageViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(MessageViewModel model)
        {
            return View("Message", model);
        }
    }
}
