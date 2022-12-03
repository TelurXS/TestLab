using TestLab.Entities;
using TestLab.Entities.Projects;

namespace TestLab.Models
{
    public class ProjectViewModel
    {
        public Project Project { get; set; }
        public Account Author { get; set; }
        public bool IsViewAllowed { get; set; }
    }
}
