using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace TestLab.DataBase
{
    public interface IRepository<T> where T : class
    {
        public DbSet<T> Collection { get; }

        public IEnumerable<T> GetAll();
        public T GetOne(int id);
    }
}
