using Bookshop.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;

namespace Bookshop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        IPKG_Books package;

        public BooksController(IPKG_Books package)
        {
            this.package = package;
        }
        [HttpPost]
        public IActionResult AddBook(string title, string author, int quantity, decimal price)
        {
            try
            {
                return Ok(package.AddBook(title, author, quantity, price));
            }
            catch (OracleException ex)
            {
                return StatusCode(500, $"Oracle error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }
        }
        [HttpGet]
        public IActionResult GetBooks()
        {
            try
            {
                return Ok(package.GetAllBooks());
            }
            catch (OracleException ex)
            {
                return StatusCode(500, $"Oracle error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }
        }
        [HttpDelete]
        public IActionResult DeleteBook(int id)
        {
            try
            {
                var operationWasSucessful = package.DeleteBook(id);
                if (!operationWasSucessful)
                {
                    return NotFound($"Book with ID {id} not found.");
                }
                return Ok();
            }
            catch (OracleException ex)
            {
                return StatusCode(500, $"Oracle error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }
        }

        [HttpPut]
        public IActionResult UpdateBookDetails(int id, string title, string author, decimal price)
        {
            try
            {
                var operationWasSucessful = package.UpdateBookDetails(id, title, author, price);
                if (!operationWasSucessful)
                {
                    return NotFound($"Book with ID {id} not found.");
                }
                return Ok();
            }
            catch (OracleException ex)
            {
                return StatusCode(500, $"Oracle error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }

        }

        [HttpPut]
        public IActionResult DecreaseBookQuantity(int id, int quantity)
        {
            try
            {
                var operationWasSucessful = package.DecreaseBookQuantity(id, quantity);
                if (!operationWasSucessful)
                {
                    return NotFound($"Book with ID {id} not found.");
                }
                return Ok();
            }
            catch (OracleException ex)
            {
                return StatusCode(500, $"Oracle error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An unexpected error occurred: {ex.Message}");
            }

        }
    }
}
