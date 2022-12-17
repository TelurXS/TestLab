using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TestLab.Entities;

namespace TestLab.DataBase
{
    public class PostLikes : IRepository<PostLike>
    {
        public PostLikes(TestLabContext context)
        {
            Context = context;
            Collection = context.PostLikes;
        }

        public TestLabContext Context { get; }
        public DbSet<PostLike> Collection { get; }

        public IEnumerable<PostLike> GetAll()
        {
            return Collection;
        }

        public PostLike GetOne(int id)
        {
            return Collection.SingleOrDefault(x => x.Id == id);
        }

        public PostLike FromAccountToPost(Account account, Post post) 
        {
            return Collection.Where(x => x.AccountId == account.Id && x.PostId == post.Id).SingleOrDefault();
        }

        public bool IsAccountLikePost(Account account, Post post) 
        {
            return FromAccountToPost(account, post) is not null;
        }

        public bool AddLike(Account account, Post post) 
        {
            Collection.Add(new PostLike { AccountId = account.Id, PostId = post.Id });
            return Save();
        }

        public bool RemoveLike(Account account, Post post)
        {
            Collection.Remove(FromAccountToPost(account, post));
            return Save();
        }

        public bool Save() 
        {
            return Context.Save();
        }

        public void ToogleLike(Account account, Post post) 
        {
            PostLike postLike = FromAccountToPost(account, post);

            if (postLike is null)
            {
                AddLike(account, post);
            }
            else 
            {
                RemoveLike(account, post);
            }
        }

        public int LikeCount(Post post) 
        {
            return Collection.Where(x => x.PostId == post.Id).Count();
        }
    }
}
