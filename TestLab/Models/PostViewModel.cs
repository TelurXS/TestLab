using TestLab.Entities;

namespace TestLab.Models
{
    public class PostViewModel
    {
        public Post Post { get; set; }
        public Account Author { get; set; }
        public bool IsLiked { get; set; }
        public int LikeCount { get; set; }
    }
}
