using TestLab.Entities.Pagination;
using TestLab.Entities;
using System.Collections;
using System.Collections.Generic;

namespace TestLab.Models
{
    public class DashboardWorkersViewModel : MessageViewModel
    {
        public PagenableCollection<Worker> Workers { get; set; }
        public IEnumerable<Account> Accounts { get; set; }

        public string SearchPattern { get; set; }
    }
}
