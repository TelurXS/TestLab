namespace TestLab.Utils.Files
{
    public class ProjectResultFileParser : FileParser
    {
        public ProjectResultFileParser()
            : base(Config.Files.ProjectsResultsDirectory)
        {
        }
    }
}
