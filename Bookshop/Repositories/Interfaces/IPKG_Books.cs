using Bookshop.Models;

namespace Bookshop.Repositories.Interfaces
{
    public interface IPKG_Books
    {
        int AddBook(string title, string author, int quantity, decimal price);
        bool UpdateBookDetails(int id, string title, string author, decimal price);
        bool DeleteBook(int id);
        List<Book> GetAllBooks();
        bool DecreaseBookQuantity(int id, int quantity);
    }
}
