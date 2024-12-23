using Bookshop.Models;
using Bookshop.Repositories.Interfaces;
using Oracle.ManagedDataAccess.Client;

namespace Bookshop.Repositories.implementations
{
    internal class PKG_Books : PKG_BASE, IPKG_Books
    {
        private string bookBase = "olerning.PK_TENGIZ_BOOKS"; 
        public PKG_Books(IConfiguration configuration) : base(configuration)
        {

        }
        public int AddBook(string title, string author, int quantity, decimal price)
        {
            using (var conn = new OracleConnection(ConnStr))
            {
                conn.Open();

                using (var cmd = new OracleCommand($"{bookBase}.add_book", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_title", OracleDbType.Varchar2).Value = title;
                    cmd.Parameters.Add("p_quantity", OracleDbType.Int32).Value = quantity;
                    cmd.Parameters.Add("p_author", OracleDbType.Varchar2).Value = author;
                    cmd.Parameters.Add("p_price", OracleDbType.Decimal).Value = price;
                    cmd.Parameters.Add("p_id", OracleDbType.Int32).Direction = System.Data.ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                    int rowId = int.Parse(cmd.Parameters["p_id"].Value.ToString());
                    return rowId;
                }

            }
        }

        public bool DecreaseBookQuantity(int id, int quantity)
        {
            using (var conn = new OracleConnection(ConnStr))
            {
                conn.Open();

                using (var cmd = new OracleCommand($"{bookBase}.decrease_book_quantity", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_id", OracleDbType.Int32).Value = id;
                    cmd.Parameters.Add("p_quantity", OracleDbType.Int32).Value = quantity;
                   
                    cmd.Parameters.Add("updated_rows_count", OracleDbType.Int32).Direction = System.Data.ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                    int affectedRowsCount = int.Parse(cmd.Parameters["updated_rows_count"].Value.ToString());


                    return affectedRowsCount > 0;
                }

            }
        }

        public bool DeleteBook(int id)
        {
            using (var conn = new OracleConnection(ConnStr))
            {
                conn.Open();

                using (var cmd = new OracleCommand($"{bookBase}.delete_book", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_id", OracleDbType.Int32).Value = id;
                    cmd.Parameters.Add("updated_rows_count", OracleDbType.Int32).Direction = System.Data.ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                    int affectedRowsCount = int.Parse(cmd.Parameters["updated_rows_count"].Value.ToString());


                    return affectedRowsCount > 0;
                }
            }
        }

        public List<Book> GetAllBooks()
        {
            using (var conn = new OracleConnection(ConnStr))
            {
                conn.Open();

                using (var cmd = new OracleCommand($"{bookBase}.get_all_books", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_result", OracleDbType.RefCursor).Direction = System.Data.ParameterDirection.Output;

                    List<Book> books = new List<Book>();

                    OracleDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Book book = new Book();
                        book.Id = reader.GetInt32(0);
                        book.Title = reader.GetString(1);
                        book.Quantity = reader.GetInt32(2);
                        book.Author = reader.GetString(3);
                        book.Price = reader.GetDecimal(4);
                        books.Add(book);
                    }
                    return books;
                }
            }
        }

        public bool UpdateBookDetails(int id, string title, string author, decimal price)
        {
            using (var conn = new OracleConnection(ConnStr))
            {
                conn.Open();

                using (var cmd = new OracleCommand($"{bookBase}.update_book_details", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("p_id", OracleDbType.Int32).Value = id;
                    cmd.Parameters.Add("p_title", OracleDbType.Varchar2).Value = title;
                    cmd.Parameters.Add("p_author", OracleDbType.Varchar2).Value = author;
                    cmd.Parameters.Add("p_price", OracleDbType.Decimal).Value = price;
                    cmd.Parameters.Add("updated_rows_count", OracleDbType.Int32).Direction = System.Data.ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                    int affectedRowsCount = int.Parse(cmd.Parameters["updated_rows_count"].Value.ToString());


                    return affectedRowsCount > 0;
                }

            }
        }
    }
}
