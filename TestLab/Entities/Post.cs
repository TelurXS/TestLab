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

        public Account Author { get; set; }

        [DataType(DataType.DateTime)] 
        public DateTime CreationDate { get; set; }
        [DataType(DataType.DateTime)] 
        public DateTime ReleaseDate { get; set; }

        public PostState State { get; set; }
    }
}
