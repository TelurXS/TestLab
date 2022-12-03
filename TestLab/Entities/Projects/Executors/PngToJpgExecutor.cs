using System;

namespace TestLab.Entities.Projects.Executors
{
    public class PngToJpgExecutor : ProjectExecutor
    {
        public PngToJpgExecutor(Project project) : base(project) { }

        public override bool Execute()
        {
            Console.WriteLine("CONVERTATION...");

            Project.Result = "SUCCESS";
            Project.State = ProjectState.Completed;

            return true;
        }
    }
}
