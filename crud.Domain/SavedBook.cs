using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace crud.Domain
{
	public class SavedBook
	{
        [Key]
        public int SavedBookId { get; set; }
        public int CustomerID { get; set; }
        public int BookId { get; set; }

        [JsonIgnore]
        public Customer Customer { get; set; }

        [JsonIgnore]
        public Book Book { get; set; }
    }
}

