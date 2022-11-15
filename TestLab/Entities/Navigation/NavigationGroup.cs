using System.Collections.Generic;

namespace TestLab.Entities
{
    public class NavigationGroup : INavigation
    {
        public NavigationGroup(Navigation root)
        {
            Id = root.Id;
            Title = root.Title;
            Href = root.Href;

            Childrens = new List<INavigation>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Href { get; set; }

        public List<INavigation> Childrens { get; private set; } 

        public bool HasChilds => Childrens.Count > 0;
    }
}
