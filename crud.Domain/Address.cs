using System;
namespace crud.Domain
{
    public class Address
    {
        public int AddressID { get; set; } // Primary Key
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
    }

}

