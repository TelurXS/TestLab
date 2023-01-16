using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using TestLab.Utils.Files;

namespace TestLab.Entities.Projects.Executors
{
    public class JpgToPngExecutor : ProjectExecutor
    {
        public JpgToPngExecutor(Project project) 
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

            if (ResourceParser.ExtensionOf(Project.Resource) == ".jpg")
            {
                string path = ResultParser.GetFullLocalPath();
                string name = ResultParser.GetUniqueFileName() + ".png";

                using (Image png = Image.FromFile(file)) 
                {
                    png.Save(path + name, ImageFormat.Png);
                }

                Project.State = ProjectState.Completed;
                Project.ResultType = ProjectResultType.Image;
                Project.Result = Config.Files.ProjectsResultsDirectory + name;
                return true;
            }

            Project.State = ProjectState.Error;
            Project.ResultType = ProjectResultType.Message;
            Project.Result = "Resource file is not jpg image";

            return false;
        }
    }
}
