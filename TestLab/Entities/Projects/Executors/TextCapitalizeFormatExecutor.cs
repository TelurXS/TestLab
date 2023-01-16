
namespace TestLab.Entities.Projects.Executors
{
    public class TextCapitalizeFormatExecutor : TextFormatExecutor
    {
        public TextCapitalizeFormatExecutor(Project project) 
            : base(project)
        {
        }

        public override string Format(string value)
        {
            for (int i = 0; i < value.Length; i++)
            {
                if (i == 0 || value[i - 1] == ' ')
                {
                    if (char.IsLower(value[i]))
                    {
                        string letter = char.ToUpper(value[i]).ToString();

                        value = value.Remove(i, 1).Insert(i, letter);
                    }
                }
            }

            return value;
        }
    }
}
