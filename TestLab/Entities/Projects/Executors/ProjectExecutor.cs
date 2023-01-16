namespace TestLab.Entities.Projects.Executors
{
    public abstract class ProjectExecutor : IProjectExecutor
    {
        public ProjectExecutor(Project project)
        {
            Project = project;
        }

        public Project Project { get; set; }

        public abstract bool Execute();
    }
}
