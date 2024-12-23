namespace Bookshop.Models
{
    public class Order
    {
        public int BookId { get; set; }
        public int Quantity {  get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string IdNumber {  get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public decimal ShippingFee {  get; set; }
    }

   
}

