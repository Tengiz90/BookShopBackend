namespace Bookshop.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Quantity { get; set; }
        public string Author { get; set; }
        public Decimal Price { get; set; }
    }
}
