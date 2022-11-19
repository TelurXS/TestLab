﻿using Microsoft.AspNetCore.Http;

namespace TestLab.Models
{
    public class FormViewModel
    {
        public string? Message { get; set; }
        public IFormCollection Form { get; set; }

        public bool HasMessage =>
            string.IsNullOrEmpty(Message) == false;
    }
}