using System.Collections.Generic;
using TestLab.Entities;
using TestLab.Entities.Pagination;
using TestLab.Entities.Projects;

namespace TestLab.Models
{
    public class DashboardProjectsViewModel : MessageViewModel
    {
        public PagenableCollection<Project> Projects { get; set; }

        public string SearchPattern { get; set; }
    }
}
