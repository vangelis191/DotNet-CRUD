using System;
using System.ComponentModel.DataAnnotations;

namespace crud.Domain
{
    public class Book
    {
        [Key]
        public Guid BookID { get; set; } // Primary Key
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public int PublicationYear { get; set; }
        public string ISBN { get; set; }
        public decimal Price { get; set; } = 2.0m;
        public bool IsAvailableForRent { get; set; }
    }
}

