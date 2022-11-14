using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TestLab.Entities;

namespace TestLab.DataBase
{
    public class Navigations : IRepository<Navigation>
    {
        public Navigations(TestLabContext context)
        {
            Collection = context.Navigations;
        }

        public DbSet<Navigation> Collection { get; }

        public IEnumerable<Navigation> GetAll()
        {
            return Collection;
        }

        public Navigation? GetOne(int id)
        {
            return Collection.SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<INavigation> AssignChildrens() 
        {
            List<INavigation> result = new List<INavigation>();             

            IEnumerable<Navigation> navigations =
                Collection.OrderBy(x => x.Parent);

            foreach (Navigation item in navigations)
            {
                IEnumerable<INavigation> childs = 
                    navigations.Where(x => x.Parent == item);

                if (childs.Count() > 0)
                {
                    NavigationGroup group = new NavigationGroup(item);

                    group.Childrens.AddRange(childs);

                    result.Add(group);
                }
                else 
                {
                    if (item.Parent is not null)
                        continue;

                    result.Add(item);
                }
            }

            return result;
        }
    }
}
