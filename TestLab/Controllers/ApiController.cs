using Microsoft.AspNetCore.Mvc;
using TestLab.DataBase;
using TestLab.Entities;

namespace TestLab.Controllers
{
    public class ApiController : Controller
    {
        public ApiController()
        {
            TestLabContext context = new TestLabContext();

            Accounts = new Accounts(context);
        }

        public Accounts Accounts { get; set; }

        [HttpGet]
        public IActionResult GetAccountById(int id) 
        {
            return Json(Accounts.GetOne(id));
        }
    }
}
