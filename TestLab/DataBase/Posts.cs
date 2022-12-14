using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TestLab.Entities;

namespace TestLab.DataBase
{
    public class Posts : IRepository<Post>
    {
        public Posts(TestLabContext context)
        {
            Context = context;
            Collection = context.Posts;
        }

        public TestLabContext Context { get; set; }
        public DbSet<Post> Collection { get; }

        public IEnumerable<Post> GetAll()
        {
            return Collection;
        }

        public Post GetOne(int id)
        {
            return Collection.SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<Post> Search(string pattern) 
        {
            return Collection.Where(x => x.Title.Contains(pattern) || x.Description.Contains(pattern));
        }

        public bool Save()
        {
            return Context.Save();
        }

        public bool Insert(Post post) 
        {
            Collection.Add(post);
            return Save();
        }
    }
}
