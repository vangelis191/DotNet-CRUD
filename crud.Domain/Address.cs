using System;
using System.ComponentModel.DataAnnotations;

namespace crud.Domain
{
    public class Address
    {
        [Key]
        public int AddressID { get; set; } // Primary Key
        public string Street { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }

        public int CustomerID { get; set; }
        public Customer? Customer { get; set; } = null;
    }

}

