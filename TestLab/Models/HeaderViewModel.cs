using System.Collections.Generic;
using System.Security.Principal;
using TestLab.Entities;

namespace TestLab.Models
{
    public class HeaderViewModel
    {
        public IEnumerable<INavigation> Navigations { get; set; }

        public Account Account { get; set; }
        public IIdentity User { get; set; }
    }
}
