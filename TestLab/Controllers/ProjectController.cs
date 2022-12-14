using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using TestLab.DataBase;
using TestLab.Entities;
using TestLab.Entities.Projects;
using TestLab.Models;
using TestLab.Utils.Files;
using TestLab.Utils.Validation;

namespace TestLab.Controllers
{
    public class ProjectController : Controller
    {
        public ProjectController()
        {
            TestLabContext context = new TestLabContext();

            Accounts = new Accounts(context);
            Projects = new Projects(context);
        }

        public Accounts Accounts { get; set; }
        public Projects Projects { get; set; }

        [HttpGet]
        public IActionResult Index(int id)
        {
            Project project = Projects.GetOne(id);

            if (project is not null)
            {
                Account author = Accounts.GetOne(project.AuthorId);
                Account user = Accounts.GetBySession(User);

                ProjectViewModel model = new ProjectViewModel
                {
                    Project = project,
                    Author = author,
                    IsViewAllowed = (project.Accessability is ProjectAccessability.Private ?
                    user?.Id == author.Id : true)
                };

                return View(model);
            }

            return View("ProjectNotFound");

        }

        [HttpGet]
        public IActionResult Create(ProjectType type)
        {
            Account account = Accounts.GetBySession(User);

            if (account is null) 
                return Redirect("/account/login");

            ProjectCreateViewModel model = new ProjectCreateViewModel
            {
                Type = type,
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Create([DefaultValue("")] string name, ProjectType type, ProjectAccessability accessability, IFormFile resource)
        {
            Account account = Accounts.GetBySession(User);

            if (account is null) 
                return Redirect("/account/login");

            ProjectCreateViewModel model = new ProjectCreateViewModel
            {
                Type = type,
            };

            if (new TextValidator(name, "Name").Max(128).IsInvalid)
            {
                model.Message = "Name max length is 128";
                return View(model);
            };

            string resourceName = "";

            if (resource is not null)
            {
                bool resourceSaveResult = new FileParser().SaveProjectResource(resource, out resourceName);

                if (resourceSaveResult is false)
                {
                    model.Message = "Resource File is invalid";
                    return View(model);
                }
            }
            else
            {
                model.Message = "Resource File is missing";
                return View(model);
            }

            int id = account.Id;

            Project project = Project.Create(name, id, resourceName, type, accessability);

            project.GetExecutor().Execute();

            if (Projects.Insert(project) is false)
            {
                model.Message = "Something wrong";
                model.MessageType = MessageType.Danger;
                return View(model);
            }

            return View("ProjectCreatedSuccessfully", project);
        }
    }
}
