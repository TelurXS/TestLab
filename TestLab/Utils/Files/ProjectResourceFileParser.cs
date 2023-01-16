namespace TestLab.Utils.Files
{
    public class ProjectResourceFileParser : FileParser
    {
        public ProjectResourceFileParser()
            : base(Config.Files.ProjectsResourcesDirectory)
        {
        }
    }
}
