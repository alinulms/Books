using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Indexing.Model;
using Sitecore.ContentSearch;

namespace Books.Model
{
  public class Book : IIndexable
  {
    public void LoadAllFields()
    {
    }

    public IIndexableDataField GetFieldById(object fieldId)
    {
      return Fields.FirstOrDefault(f => f.Id == fieldId);
    }

    public IIndexableDataField GetFieldByName(string fieldName)
    {
      return Fields.FirstOrDefault(f => f.Name == fieldName);
    }

    public virtual IIndexableId Id
    {
      get;
      set;
    }
    public virtual IIndexableUniqueId UniqueId { get; set; }
    public virtual string DataSource { get; set; }
    public virtual string AbsolutePath { get; set; }
    public CultureInfo Culture { get; set; }

    private IEnumerable<IIndexableDataField> _fields;
    public IEnumerable<IIndexableDataField> Fields
    {
      get
      {
        return _fields ?? (_fields = GetIndexableProperties());
      }

    }

    private IEnumerable<IIndexableDataField> GetIndexableProperties()
    {
      return (from property in GetType().GetProperties()
              where property.GetCustomAttribute<IndexFieldAttribute>() != null
              select new IndexableField(property, property.GetValue(this))).ToArray();
    }

    [IndexField("Author")]
    public string Author { get; set; }

    [IndexField("Title")]
    public string Title { get; set; }

    [IndexField("Genre")]
    public string Genre { get; set; }

    [IndexField("Price")]
    public double Price { get; set; }

    [IndexField("PublishDate")]
    public string PublishDate { get; set; }

    [IndexField("Description")]
    public string Description { get; set; }
  }
}