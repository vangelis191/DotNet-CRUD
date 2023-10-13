using System;
using System.ComponentModel.DataAnnotations;

namespace crud.Domain
{
	public class Customer
	{
        [Key]
        public Guid CustomerID { get; set; } // Primary Key
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Address Address { get; set; }
        public ICollection<Order> Orders { get; set; } // A customer can have multiple orders
    }
}

