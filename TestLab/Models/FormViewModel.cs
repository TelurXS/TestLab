using Microsoft.AspNetCore.Http;

namespace TestLab.Models
{
    public class FormViewModel
    {
        public IFormCollection Form { get; set; }

        public string? Message { get; set; }
        public bool HasMessage => string.IsNullOrEmpty(Message) is false;
    }
}
