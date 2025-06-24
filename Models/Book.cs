namespace BookstoreXmlApi.Models
{
    public class Book
    {
        public required string ISBN { get; set; }
        public required string Title { get; set; }
        public required List<string> Authors { get; set; }
        public required string Category { get; set; }
        public string? Cover { get; set; }
        public int Year { get; set; }
        public decimal Price { get; set; }
    }
}
