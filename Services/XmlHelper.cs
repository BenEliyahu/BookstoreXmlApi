using BookstoreXmlApi.Models;
using System.Xml.Linq;

namespace BookstoreXmlApi.Services
{
    public static class XmlHelper
    {
        public static Book ToBook(XElement x)
        {
            return new Book
            {
                ISBN = x.Element("isbn")?.Value ?? string.Empty,
                Title = x.Element("title")?.Value ?? string.Empty,
                Authors = x.Elements("author").Select(a => a.Value).ToList(),
                Category = x.Attribute("category")?.Value ?? string.Empty,
                Cover = x.Attribute("cover")?.Value,
                Year = int.TryParse(x.Element("year")?.Value, out int y) ? y : 0,
                Price = decimal.TryParse(x.Element("price")?.Value, out decimal p) ? p : 0
            };
        }

        public static XElement ToXElement(Book b)
        {
            var el = new XElement("book",
                new XAttribute("category", b.Category),
                !string.IsNullOrEmpty(b.Cover) ? new XAttribute("cover", b.Cover) : null,
                new XElement("isbn", b.ISBN),
                new XElement("title", b.Title),
                b.Authors.Select(a => new XElement("author", a)),
                new XElement("year", b.Year),
                new XElement("price", b.Price)
            );

            return el;
        }
    }
}