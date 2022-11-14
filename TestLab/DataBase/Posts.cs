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
            Collection = context.Posts;
        }

        public DbSet<Post> Collection { get; }

        public IEnumerable<Post> GetAll()
        {
            return Collection;
        }

        public Post GetOne(int id)
        {
            return Collection.SingleOrDefault(x => x.Id == id);
        }
    }
}
