using System.IO;
using TestLab.Utils.Files;

namespace TestLab.Entities.Projects.Executors
{
    public abstract class TextFormatExecutor : ProjectExecutor
    {
        public TextFormatExecutor(Project project)
            : base(project)
        {
            ResourceParser = new ProjectResourceFileParser();
            ResultParser = new ProjectResultFileParser();
        }

        public IFileParser ResourceParser { get; private set; }
        public IFileParser ResultParser { get; private set; }

        public override bool Execute()
        {
            string file = ResourceParser.FullPathOf(Project.Resource);

            if (File.Exists(file) is false)
            {
                Project.State = ProjectState.Error;
                Project.ResultType = ProjectResultType.Message;
                Project.Result = "Something wrong";
                return false;
            }

            if (ResourceParser.ExtensionOf(Project.Resource) == ".txt")
            {
                string text = ResourceParser.Read(Project.Resource);

                ResultParser.Save(Format(text), "txt", out string path);

                Project.State = ProjectState.Completed;
                Project.ResultType = ProjectResultType.File;
                Project.Result = path;
                return true;
            }

            Project.State = ProjectState.Error;
            Project.ResultType = ProjectResultType.Message;
            Project.Result = "Resource is not a txt file";

            return false;
        }

        public abstract string Format(string value);
    }
}
