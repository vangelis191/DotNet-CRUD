using System;
namespace crud.Data
{
    using System.Collections.Generic;
    using System.Reflection.Emit;
    using System.Reflection.Metadata;
    using crud.Domain;
    using Microsoft.EntityFrameworkCore;


    public class UserDbContext : DbContext
    {
        private const string _connectionString = "User ID =postgres;Password=Gold2023;Server=localhost;Port=5432;Database=postgres";

        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<SavedBook> SavedBooks { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString, builder => builder.MigrationsAssembly("crud.Api"));

            if (!optionsBuilder.IsConfigured) optionsBuilder.UseNpgsql(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Customer>()
            .HasOne(e => e.Address)
                .WithOne(e => e.Customer)
            .HasForeignKey<Address>(e => e.CustomerID)
            .IsRequired();


            // Configure the many-to-many relationship between User and Book through SavedBook
            modelBuilder.Entity<SavedBook>()
                .HasKey(sb => new { sb.CustomerID, sb.BookId });

            modelBuilder.Entity<SavedBook>()
                .HasOne(sb => sb.Customer)
                .WithMany(u => u.SavedBook)
                .HasForeignKey(sb => sb.CustomerID);

            modelBuilder.Entity<SavedBook>()
                .HasOne(sb => sb.Book)
                .WithMany(b => b.SavedBook)
                .HasForeignKey(sb => sb.BookId);

        }


    }
}

