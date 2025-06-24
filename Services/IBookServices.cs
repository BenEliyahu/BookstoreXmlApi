using BookstoreXmlApi.Models;

namespace BookstoreXmlApi.Services
{
    public interface IBookService
    {
        List<Book> GetAll();
        Book? GetByIsbn(string isbn);
        void Add(Book book);
        bool Update(string isbn, Book book);
        bool Delete(string isbn);
        string GenerateHtmlReport();
    }
}