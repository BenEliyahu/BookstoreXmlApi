using BookstoreXmlApi.Models;
using BookstoreXmlApi.Configuration;
using Microsoft.Extensions.Options;
using System.Xml.Linq;

namespace BookstoreXmlApi.Services
{
    public class BookService : IBookService
    {
        private readonly string _filePath;

        public BookService(IOptions<XmlSettings> options)
        {
            _filePath = options.Value.FilePath;
        }

        public List<Book> GetAll()
        {
            var xdoc = XDocument.Load(_filePath);
            return xdoc.Descendants("book").Select(XmlHelper.ToBook).ToList();
        }

        public Book? GetByIsbn(string isbn)
        {
            return GetAll().FirstOrDefault(b => b.ISBN == isbn);
        }

        public void Add(Book newBook)
        {
            var books = LoadBooks();

            if (books.Any(b => b.ISBN == newBook.ISBN))
                throw new Exception($"Book with ISBN {newBook.ISBN} already exists.");

            books.Add(newBook);
            SaveBooks(books);
        }

        public bool Delete(string isbn)
        {
            var books = LoadBooks();
            var bookToRemove = books.FirstOrDefault(b => b.ISBN == isbn);

            if (bookToRemove == null)
                return false;

            books.Remove(bookToRemove);
            SaveBooks(books);
            return true;
        }

        public bool Update(string isbn, Book updatedBook)
        {
            var books = LoadBooks();
            var index = books.FindIndex(b => b.ISBN == isbn);

            if (index == -1)
                return false;

            books[index] = updatedBook;
            SaveBooks(books);
            return true;
        }

        public string GenerateHtmlReport()
        {
            var books = GetAll();
            var html = "<html><head><style>table,th,td{border:1px solid black;padding:5px;border-collapse:collapse;}</style></head><body>";
            html += "<h2>Bookstore Report</h2><table><tr><th>Title</th><th>Author(s)</th><th>Category</th><th>Year</th><th>Price</th></tr>";

            foreach (var book in books)
            {
                html += $"<tr><td>{book.Title}</td><td>{string.Join(", ", book.Authors)}</td><td>{book.Category}</td><td>{book.Year}</td><td>{book.Price}</td></tr>";
            }

            html += "</table></body></html>";
            return html;
        }

        private List<Book> LoadBooks()
        {
            var xdoc = XDocument.Load(_filePath);
            return xdoc.Descendants("book").Select(XmlHelper.ToBook).ToList();
        }

        private void SaveBooks(List<Book> books)
        {
            var xdoc = new XDocument(
                new XElement("bookstore",
                    books.Select(XmlHelper.ToXElement)
                )
            );
            xdoc.Save(_filePath);
        }
    }
}