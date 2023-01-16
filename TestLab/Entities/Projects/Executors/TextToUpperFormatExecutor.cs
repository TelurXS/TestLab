
namespace TestLab.Entities.Projects.Executors
{
    public class TextToUpperFormatExecutor : TextFormatExecutor
    {
        public TextToUpperFormatExecutor(Project project) 
            : base(project)
        {
        }

        public override string Format(string value)
        {
            return value.ToUpper();
        }
    }
}
