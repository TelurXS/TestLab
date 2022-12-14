using TestLab.Entities.Projects;

namespace TestLab.Models
{
    public class ProjectCreateViewModel : MessageViewModel
    {
        public ProjectType Type { get; set; }
    }
}
