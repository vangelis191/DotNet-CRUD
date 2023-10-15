using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace crud.Domain
{
	public class Order
	{
        [Key]
        public int OrderID { get; set; } // Primary Key
        public int CustomerID { get; set; } // Foreign Key to Customers
        public DateTime Timestamp { get; set; }
        public DateTime OrderDate { get; set; }


        public List<Book> Books { get; set; } // An order can directly contain multiple books
        [JsonIgnore]
        public Customer? Customer { get; set; } // Navigation property to link to the customer
    }
}

