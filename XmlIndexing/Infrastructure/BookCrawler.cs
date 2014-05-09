using System.Linq;
using System.Xml.Linq;
using Books.Model.Factories;
using Indexing.Model;
using Sitecore.ContentSearch;
using Sitecore.Events;
using Sitecore.Globalization;
using Sitecore.Data.Items;
using Sitecore.ContentSearch.Diagnostics;

namespace Books.Infrastructure
{
  public class BookCrawler : SitecoreItemCrawler
  {
    protected override void DoAdd(IProviderUpdateContext context, SitecoreIndexableItem indexable)
    {
      Operations = new CustomIndexOperations();
      Sitecore.Diagnostics.Assert.ArgumentNotNull(context, "context");
      Sitecore.Diagnostics.Assert.ArgumentNotNull(indexable, "indexable");
      Event.RaiseEvent("indexing:adding", new object[] { context.Index.Name, indexable.UniqueId });
      if (!IsExcludedFromIndex(indexable))
      {
        Language[] languages = indexable.Item.Languages;
        for (int i = 0; i < languages.Length; i = i + 1)
        {
          Language language = languages[i];
          Item item = indexable.Item.Database.GetItem(indexable.Item.ID, language, Sitecore.Data.Version.Latest);
          if (item == null)
          {
            CrawlingLog.Log.Warn(string.Format("LuceneIndexOperations : AddItem : Could not build document data {0} - Latest version could not be found. Skipping.", indexable.Item.Uri), null);
          }
          else
          {
            Item[] versions = item.Versions.GetVersions(false);
            for (int j = 0; j < versions.Length; j = j + 1)
            {
              Item itemVersion = versions[j];
              SitecoreIndexableItem indexableItem = itemVersion;
              IIndexableBuiltinFields fields = indexableItem;
              fields.IsLatestVersion = fields.Version == item.Version.Number;
              indexableItem.IndexFieldStorageValueFormatter = context.Index.Configuration.IndexFieldStorageValueFormatter;


              if (indexable.Item.Template.BaseTemplates.FirstOrDefault(t => t.ID == Constants.Tempaltes.BooksData) == null)
                return;
              BookFactory bookFactory = new BookFactory();

              XDocument doc = XDocument.Parse(indexable.Item.Fields[Constants.Field.Books].Value);
              var catalog = doc.Element("catalog");
              foreach (XElement descendant in catalog.Descendants("book"))
              {
                var book = bookFactory.Create(indexable.Item, descendant);
                Operations.Add(book, context, index.Configuration);
              }
            }
          }
        }
        Event.RaiseEvent("indexing:added", new object[] { context.Index.Name, indexable.UniqueId });
      }
    }
  }
}