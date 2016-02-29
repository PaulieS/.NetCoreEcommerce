using System;
using System.Linq;

namespace SearchProvider
{
    public interface ISearchProvider
    {

        IQueryable<T> Search<T>(IQueryable<T> searchedObject, string searchPhrase,
            params Func<T, string>[] searchedFileds);

    }
}