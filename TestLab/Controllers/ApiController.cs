using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
            Posts = new Posts(context);
            Projects = new Projects(context);
        }

        public Accounts Accounts { get; set; }
        public Posts Posts { get; set; }
        public Projects Projects { get; set; }

        [HttpGet]
        public IActionResult GetAccountById(int id) 
        {
            return Json(Accounts.GetOne(id));
        }

        [HttpGet]
        public IActionResult GetPostById(int id) 
        {
            return Json(Posts.GetOne(id));
        }

        [HttpGet]
        public IActionResult GetProjectsGroupedByDay() 
        {
            return Json(Projects.Collection.GroupBy(x => x.CreationDate.Day));
        }

        [HttpGet]
        public IActionResult GetProjectsTypesStats()
        {
            List<KeyValuePair<string, int>> projects = new List<KeyValuePair<string, int>>();

            foreach (var item in Projects.Collection.GroupBy(x => x.Type))
            {
                projects.Add(new KeyValuePair<string, int>(item.Key.ToString(), item.Count()));
            }

            return Json(projects);
        }

        [HttpGet]
        public IActionResult GetAdministrators()
        {
            return Json(Accounts.Collection.Where(x => x.State == AccountState.Administrator));
        }

        [HttpGet]
        public IActionResult GetPostsGroupedByDay()
        {
            List<KeyValuePair<DateTime, int>> posts = new List<KeyValuePair<DateTime, int>>();

            foreach (var item in Posts.Collection.GroupBy(x => x.CreationDate.Date))
            {
                posts.Add(new KeyValuePair<DateTime, int>(item.Key, item.Count()));
            }

            return Json(posts);
        }
    }
}
