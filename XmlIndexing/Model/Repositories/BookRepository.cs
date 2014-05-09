using System.Collections.Generic;
using System.Linq;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Linq.Utilities;

namespace Books.Model.Repositories
{
  public class BookRepository
  {
    public static IEnumerable<Book> Get()
    {
      var index = ContentSearchManager.GetIndex("book_index");
      using (var context = index.CreateSearchContext())
      {
        var results =  context.GetQueryable<Book>().ToList();
        return results;
      }
    }

    public static IEnumerable<Book> GetByName(string name)
    {
      var index = ContentSearchManager.GetIndex("book_index");
      using (var context = index.CreateSearchContext())
      {
        var predicate = PredicateBuilder.True<Book>();
        predicate.Or(book => book.Author.ToLower().Contains(name.ToLower()));
        return context.GetQueryable<Book>().Where(predicate).ToList();
      }
    }
  }
}