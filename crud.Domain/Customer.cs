using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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


        [JsonIgnore]
        public Address? Address { get; set; }

        [JsonIgnore]
        public ICollection<Order>? Orders { get; set; } = null!;
        public List<SavedBook>? SavedBook { get; } = new();
    }
}

