using System;
using System.ComponentModel.DataAnnotations;

namespace crud.Domain
{
	public class Customer
	{
        [Key]
        public int CustomerID { get; set; } // Primary Key
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public int AddressID { get; set; }
        public Address Address { get; set; }
        public ICollection<Order> Orders { get; set; } = null!;
    }
}

