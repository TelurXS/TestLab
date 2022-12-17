using System.Collections.Generic;
using TestLab.Entities;

namespace TestLab.Models
{
    public class DashboardSidebarViewModel
    {
        public IEnumerable<Navigation> Navigations { get; set; }
        public string OpenedTab { get; set; }
    }
}
