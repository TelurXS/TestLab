using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TestLab.Models;

namespace TestLab.Entities.Pagination
{
    public sealed class PagenableCollection<T> : IEnumerable<T>
    {
        public PagenableCollection(IEnumerable<T> items, int countPerPage, int currentPage)
        {
            Items = items;
            CountPerPage = countPerPage;
            CurrentPage = currentPage;
        }

        public IEnumerable<T> Items { get; private set; }
        public int CountPerPage { get; private set; }
        public int CurrentPage { get; private set; }

        public int FirstPage => 1;
        public int LastPage => PageCount;
        public int PageCount =>
            (int)Math.Round(Items.Count() / (float)CountPerPage, MidpointRounding.ToPositiveInfinity);

        public PaginationViewModel ViewModel =>
            new PaginationViewModel 
            { 
                FirstPage = FirstPage, 
                LastPage = LastPage, 
                CurrentPage = CurrentPage, 
                Pages = Pages,
                CountPerPage = CountPerPage, 
                PossibleCounts = new List<int> { 9, 6, 3, 20, 50 }
            };

        public IEnumerable<int> Pages => 
            Enumerable.Range(1, PageCount);

        public IEnumerable<T> GetByPage(int page) 
        {
            return Items.Skip(CountPerPage * (page - 1)).Take(CountPerPage);
        }

        public IEnumerable<T> GetByCurrentPage() => GetByPage(CurrentPage);

        public IEnumerator<T> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Items.GetEnumerator();
        }
    }
}
