using Microsoft.AspNetCore.Http;

namespace TestLab.Models
{
    public class FormViewModel : MessageViewModel
    {
        public IFormCollection Form { get; set; }
    }
}
