namespace TestLab.Entities.Projects.Executors
{
    public class ProjectExecutor : IProjectExecutor
    {
        public ProjectExecutor(Project project)
        {
            Project = project;
        }

        public Project Project { get; set; }

        public virtual bool Execute() { return false; }
    }
}
