using System.Collections.Generic;

namespace TestLab.Models
{
    public class PaginationViewModel
    {
        public int CurrentPage { get; set; }
        public IEnumerable<int> Pages { get; set; }

        public int CountPerPage { get; set; }
        public IEnumerable<int> PossibleCounts { get; set; }

        public int FirstPage { get; set; }
        public int LastPage { get; set; }
        public int NextPage { get; set; }
        public int PreviousPage { get; set; }

        public string RedirectTo { get; set; }
    }
}
