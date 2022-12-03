namespace TestLab.Entities.Projects.Executors
{
    public interface IProjectExecutor
    {
        public Project Project { get; }

        public bool Execute();
    }
}
