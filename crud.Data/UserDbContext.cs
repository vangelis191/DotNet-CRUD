using System;
namespace crud.Data
{
    using System.Collections.Generic;
    using System.Reflection.Emit;
    using crud.Domain;
    using Microsoft.EntityFrameworkCore;


    public class UserDbContext : DbContext
    {
        private const string _connectionString = "User ID =postgres;Password=Gold2023;Server=localhost;Port=5432;Database=postgres";

        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) optionsBuilder.UseNpgsql(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Add any custom configurations or relationships here if needed
        }
    }
}

