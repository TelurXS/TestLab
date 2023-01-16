
namespace TestLab.Entities.Projects.Executors
{
    public class TextToLowerFormatExecutor : TextFormatExecutor
    {
        public TextToLowerFormatExecutor(Project project) 
            : base(project)
        {
        }

        public override string Format(string value)
        {
            return value.ToLower();
        }
    }
}
