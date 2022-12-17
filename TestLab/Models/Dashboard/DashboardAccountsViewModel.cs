using System.Collections.Generic;
using TestLab.Entities;
using TestLab.Entities.Pagination;

namespace TestLab.Models
{
    public class DashboardAccountsViewModel : MessageViewModel
    {
        public PagenableCollection<Account> Accounts { get; set; }

        public string SearchPattern { get; set; }
    }
}
