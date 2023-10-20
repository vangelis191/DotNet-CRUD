using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace crud.Domain
{
	public class Order
	{
        [Key]
        public int OrderID { get; set; } 
        public int CustomerID { get; set; } 
        public DateTime Timestamp { get; set; }
        public DateTime OrderDate { get; set; }
        public List<Book> Books { get; set; }

        [JsonIgnore]
        public Customer? Customer { get; set; } // Navigation property to link to the customer
    }
}

