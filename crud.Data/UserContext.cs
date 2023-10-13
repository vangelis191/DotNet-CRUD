using System;
namespace crud.Data
{
    using System.Collections.Generic;
    using System.Reflection.Emit;
    using crud.Domain;
    using Microsoft.EntityFrameworkCore;


    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderHistory> OrderHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Add any custom configurations or relationships here if needed
        }
    }
}

