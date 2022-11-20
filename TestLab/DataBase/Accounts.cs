using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using TestLab.Entities;

namespace TestLab.DataBase
{
    public class Accounts : IRepository<Account>
    {
        public Accounts(TestLabContext context)
        {
            Context = context;
            Collection = context.Accounts;
        }

        public TestLabContext Context { get; }
        public DbSet<Account> Collection { get; }

        public IEnumerable<Account> GetAll()
        {
            return Collection;
        }

        public Account? GetOne(int id)
        {
            return Collection.SingleOrDefault(x => x.Id == id);
        }

        public Account? GetOneByLogin(string login) 
        {
            return Collection.SingleOrDefault(x => x.Login == login);
        }

        public Account GetBySession(ClaimsPrincipal claim) 
        {
            return GetOneByLogin(claim.Identity.Name);
        } 

        public bool IsExsist(int id, out Account account) 
        {
            account = GetOne(id);

            return account is not null;
        }

        public bool IsNotExsist(int id)
        {
            return GetOne(id) is null;
        }

        public bool IsExsist(string login, out Account account) 
        {
            account = GetOneByLogin(login);

            return account is not null;
        }

        public bool IsNotExsist(string login)
        {
            return GetOneByLogin(login) is null;
        }

        public bool Insert(Account account) 
        {
            Collection.Add(account);
            return Context.Save();
        }

        public bool Save() 
        {
            return Context.Save();
        }
    }
}
