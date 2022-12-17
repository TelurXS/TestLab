using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TestLab.Entities;

namespace TestLab.DataBase
{
    public class Workers : IRepository<Worker>
    {
        public Workers(TestLabContext context)
        {
            Context = context;
            Collection = context.Workers;
        }

        public TestLabContext Context { get; set; }
        public DbSet<Worker> Collection { get; set; }

        public IEnumerable<Worker> GetAll()
        {
            return Collection.Include(x => x.Account);
        }

        public IEnumerable<Worker> Search(string pattern) 
        {
            return Collection.Where(x => x.Profession.Contains(pattern));
        }

        public Worker GetOne(int id)
        {
            return Collection.SingleOrDefault(x => x.Id == id);
        }

        public bool Save() 
        {
            return Context.Save();
        }

        public bool Insert(Worker worker) 
        {
            Collection.Add(worker);
            return Save();
        }

        public bool Delete(Worker worker) 
        {
            Collection.Remove(worker);
            return Save();
        }
    }
}
