using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductsService.Model;

namespace SearchProvider
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class SearchProvider : ISearchProvider
    {
      

        public IQueryable<T> Search<T>(IQueryable<T> searchedObject, string searchPhrase, params Func<T, string>[] searchedFileds)
        {
            searchPhrase = searchPhrase.ToLower();
            var results = new List<IQueryable<T>>(searchedFileds.Length);
            results.AddRange(searchedFileds.Select(field => searchedObject.Where(x => field(x).ToLower().Contains(searchPhrase))));
            IQueryable<T> result = Enumerable.Empty<T>().AsQueryable();
            return results.Aggregate(result, (current, searchResult) => current.Union(searchResult));
        }
    }
}
