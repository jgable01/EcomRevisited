using EcomRevisited.Models;
using Microsoft.EntityFrameworkCore;

namespace EcomRevisited.Data
{
    public class EcomDbContext : DbContext
    {
        public EcomDbContext(DbContextOptions<EcomDbContext> options) : base(options)
        {
        }
        public DbSet<Models.Cart> Carts { get; set; }
        public DbSet<Models.Country> Countries { get; set; }
        public DbSet<Models.Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed Products
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = Guid.NewGuid(), Name = "Laptop", Description = "High performance laptop", AvailableQuantity = 10, Price = 1000.0 },
                new Product { Id = Guid.NewGuid(), Name = "Smartphone", Description = "Latest model", AvailableQuantity = 20, Price = 800.0 },
                new Product { Id = Guid.NewGuid(), Name = "Headphones", Description = "Wireless", AvailableQuantity = 30, Price = 150.0 },
                new Product { Id = Guid.NewGuid(), Name = "Camera", Description = "Digital SLR", AvailableQuantity = 5, Price = 1200.0 },
                new Product { Id = Guid.NewGuid(), Name = "Smartwatch", Description = "With fitness tracking", AvailableQuantity = 15, Price = 250.0 }
            );

            // Seed Countries
            modelBuilder.Entity<Country>().HasData(
                new Country { Id = Guid.NewGuid(), Name = "Canada", ConversionRate = 1.0, TaxRate = 0.07 },
                new Country { Id = Guid.NewGuid(), Name = "United States", ConversionRate = 1.1, TaxRate = 0.05 },
                new Country { Id = Guid.NewGuid(), Name = "United Kingdom", ConversionRate = 1.3, TaxRate = 0.1 }
            );
        }
    }
}
