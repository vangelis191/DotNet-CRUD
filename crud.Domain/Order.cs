using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace crud.Domain
{
	public class Order
	{
        [Key]
        public Guid OrderID { get; set; } // Primary Key
        public Guid CustomerID { get; set; } // Foreign Key to Customers
        public DateTime OrderDate { get; set; }
        public List<Book> Books { get; set; } // An order can directly contain multiple books

        [JsonIgnore]
        public Customer? Customer { get; set; } // Navigation property to link to the customer
    }
}

