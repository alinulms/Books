using System;
using System.Collections.Generic;
using Books.Model;
using Books.Model.Repositories;

namespace Books.Presentation
{
  public partial class BooksList : System.Web.UI.UserControl
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      DataBind();
    }

    public IEnumerable<Book> AllBooks
    {
      get { return BookRepository.Get(); }
    }

    public IEnumerable<Book> BookByName
    {
      get { return BookRepository.GetByName("peter"); }
    } 
  }
}