using BookstoreXmlApi.Models;
using BookstoreXmlApi.Services;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace BookstoreXmlApi.Tests
{
    public class BookServiceTests
    {
        private readonly BookService _service;
        private readonly string _testFilePath = "Data/test-bookstore.xml";

        public BookServiceTests()
        {
            var options = Options.Create(new BookstoreXmlApi.Configuration.XmlSettings
            {
                FilePath = _testFilePath
            });

            if (!File.Exists(_testFilePath))
            {
                Directory.CreateDirectory("Data");
                File.Copy("Data/bookstore.xml", _testFilePath);
            }

            _service = new BookService(options);
        }

        [Fact]
        public void GetAll_ReturnsAllBooks()
        {
            var books = _service.GetAll();
            Assert.NotEmpty(books);
        }

        [Fact]
        public void Add_And_Delete_Works()
        {
            var book = new Book
            {
                ISBN = "0000000000000",
                Title = "Test Book",
                Authors = new List<string> { "Test Author" },
                Category = "test",
                Year = 2025,
                Price = 123.45m
            };

            _service.Add(book);

            var added = _service.GetByIsbn(book.ISBN);
            Assert.NotNull(added);

            var deleted = _service.Delete(book.ISBN);
            Assert.True(deleted);

            var afterDelete = _service.GetByIsbn(book.ISBN);
            Assert.Null(afterDelete);
        }

        [Fact]
        public void Update_Works()
        {
            var book = _service.GetAll().First();
            var originalTitle = book.Title;
            var newTitle = originalTitle + " Updated";

            book.Title = newTitle;
            var updated = _service.Update(book.ISBN, book);
            Assert.True(updated);

            var updatedBook = _service.GetByIsbn(book.ISBN);
            Assert.NotNull(updatedBook);
            Assert.Equal(newTitle, updatedBook!.Title);

            book.Title = originalTitle;
            _service.Update(book.ISBN, book);
        }
    }
}
