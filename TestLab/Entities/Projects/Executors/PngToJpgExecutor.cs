﻿using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using TestLab.Utils.Files;

namespace TestLab.Entities.Projects.Executors
{
    public class PngToJpgExecutor : ProjectExecutor
    {
        public PngToJpgExecutor(Project project) : base(project) { }

        public override bool Execute()
        {
            FileParser parser = new FileParser();

            if(parser.ExtensionOf(Project.Resource) == ".png") 
            {
                string file = parser.FullPathOf(Project.Resource);

                if (File.Exists(file) is false) 
                {
                    Project.State = ProjectState.Error;
                    Project.ResultType = ProjectResultType.Message;
                    Project.Result = "Something wrong";
                    return false;
                }

                Image png = Image.FromFile(file);
                string path = parser.GetDirectory() + "/wwwroot" + Config.Files.ProjectsResultsDirectory;
                string name = parser.UniqueFileName() + ".jpg";

                png.Save(path + name, ImageFormat.Jpeg);
                png.Dispose();

                Project.State = ProjectState.Completed;
                Project.ResultType = ProjectResultType.Image;
                Project.Result = Config.Files.ProjectsResultsDirectory + name;
                return true;
            }

            Project.State = ProjectState.Error;
            Project.ResultType = ProjectResultType.Message;
            Project.Result = "Resource file is not png image";

            return false;
        }
    }
}
