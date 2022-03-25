// this creates the paging effect on the student tab
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Mvc
{
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; private set; }  // current page number
        public int TotalPages { get; private set; } // total number of pages

        // constructor for PaginatedList (sets variables)
        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex; // set page index to given value
            TotalPages = (int)Math.Ceiling(count / (double)pageSize); // derive total pages

            this.AddRange(items);
        }

        // check if there's a previous page
        public bool HasPreviousPage => PageIndex > 1;

        // check if there's a next page
        public bool HasNextPage => PageIndex < TotalPages;

        // takes page size and page number and applies the appropriate Skip
        // and Take statements to the IQueryable
        // CreateAsync method is used instead of a constructor to create the
        // PaginatedList<T> object because constructors can't run asynchronous code
        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            // Calling the ToListAsync method will return a List containing only the requested page
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
    }
}
