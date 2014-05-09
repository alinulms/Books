using Sitecore.Data;

namespace Books
{
    public class Constants
    {
        public struct Tempaltes{
            public static readonly ID BooksPage = new ID("{B8C408C7-C73D-40CB-A955-D634F50358BD}");
            public static readonly ID BooksData = new ID("{AFE423AE-CEA9-4686-A8C2-9736BEC08197}");
        }

        public struct Field
        {
            public static readonly ID Books = new ID("{9C3046AE-5B61-4DC6-8E5B-B41BC7FC6F7C}");
        }        
    }
}