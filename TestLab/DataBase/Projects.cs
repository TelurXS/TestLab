using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TestLab.Entities.Projects;

namespace TestLab.DataBase
{
    public class Projects : IRepository<Project>
    {
        public Projects(TestLabContext context)
        {
            Collection = context.Projects;
            Context = context;
        }

        public DbSet<Project> Collection { get; set; }
        public TestLabContext Context { get; set; }

        public IEnumerable<Project> GetAll()
        {
            return Collection;
        }

        public Project GetOne(int id)
        {
            return Collection.SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<Project> GetByAuthor(int authorId) 
        {
            return Collection.Where(x => x.AuthorId == authorId);
        }

        public bool Save() 
        {
            return Context.Save();
        }

        public bool Insert(Project project) 
        {
            Collection.Add(project);
            return Save();
        }

        public bool Remove(Project project) 
        {
            Collection.Remove(project);
            return Context.Save();
        }
    }
}
