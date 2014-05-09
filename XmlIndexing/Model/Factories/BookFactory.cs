using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;
using Sitecore.ContentSearch;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;

namespace Books.Model.Factories
{
  public class BookFactory
  {
    public Book Create(Item item, XElement book)
    {
      var bookElements = book.Elements();
      Assert.IsNotNull(bookElements,"bookElements");
      
      return new Book()
      {
        Author = bookElements.FirstOrDefault(e => e.Name.ToString().ToLower() == "author") == null ? string.Empty : bookElements.FirstOrDefault(e => e.Name.ToString().ToLower() == "author").Value,
        Id = new SitecoreItemId(item.ID),
        Description = bookElements.FirstOrDefault(e => e.Name.ToString().ToLower() == "description") == null ? string.Empty : bookElements.FirstOrDefault(e => e.Name.ToString().ToLower() == "description").Value,
        Genre = bookElements.FirstOrDefault(e => e.Name.ToString().ToLower() == "genre") == null ? string.Empty : bookElements.FirstOrDefault(e => e.Name.ToString().ToLower() == "genre").Value,
        AbsolutePath = item.Paths.ContentPath,
        Price = bookElements.FirstOrDefault(e => e.Name.ToString().ToLower() == "price") == null ? 0.0 : double.Parse(bookElements.FirstOrDefault(e => e.Name.ToString().ToLower() == "price").Value),
        Title = bookElements.FirstOrDefault(e => e.Name.ToString().ToLower() == "title") == null ? string.Empty : bookElements.FirstOrDefault(e => e.Name.ToString().ToLower() == "title").Value,
        Culture = CultureInfo.CurrentCulture,
        DataSource = item.ID.ToString(),
        UniqueId = new SitecoreItemUniqueId(item.Uri),
        PublishDate = bookElements.FirstOrDefault(e => e.Name.ToString().ToLower() == "publish_date") == null ? string.Empty : bookElements.FirstOrDefault(e => e.Name.ToString().ToLower() == "publish_date").Value
      };
    }
  }
}