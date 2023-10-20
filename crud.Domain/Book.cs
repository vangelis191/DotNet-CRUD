using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using System.Text.Json.Serialization;

namespace crud.Domain
{
    public class Book
    {
        [Key]
        public int BookID { get; set; } // Primary Key
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public int PublicationYear { get; set; }
        public string ISBN { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; } = 2.0m;
        public bool IsAvailableForRent { get; set; } = true;

        [JsonIgnore]
        public Order? Order { get; set; } = null!;
        public List<SavedBook> SavedBook { get; } = new();
    }

}

