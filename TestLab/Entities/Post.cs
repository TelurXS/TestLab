using System;
using System.ComponentModel.DataAnnotations;

namespace TestLab.Entities
{
    public enum PostState 
    {
        Created,
        Published,
        Deleted,
    }

    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }

        public int AuthorId { get; set; }

        [DataType(DataType.DateTime)] 
        public DateTime CreationDate { get; set; }
        [DataType(DataType.DateTime)] 
        public DateTime ReleaseDate { get; set; }

        public PostState State { get; set; }

        public static Post Create(string title, string description, string content, int authorId) 
        {
            return new Post
            {
                Title = title,
                Description = description,
                Content = content,
                Image = Config.Posts.DefaultPostImage,
                AuthorId = authorId,
                CreationDate = DateTime.Now,
                ReleaseDate = DateTime.Now,
                State = PostState.Created,
            };
        }
    }
}
