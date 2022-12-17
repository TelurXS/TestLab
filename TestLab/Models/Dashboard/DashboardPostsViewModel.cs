using TestLab.Entities;
using TestLab.Entities.Pagination;

namespace TestLab.Models
{
    public class DashboardPostsViewModel : MessageViewModel
    {
        public PagenableCollection<Post> Posts { get; set; }

        public string SearchPattern { get; set; }
    }
}
