using System;
using System.Collections.Generic;

namespace TestLab.Entities
{
    public class Navigation : INavigation
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Href { get; set; }
        public Navigation? Parent { get; set; }
    }
}
