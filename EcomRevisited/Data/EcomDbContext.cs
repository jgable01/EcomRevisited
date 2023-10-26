using EcomRevisited.Models;
using Microsoft.EntityFrameworkCore;

namespace EcomRevisited.Data
{
    public class EcomDbContext : DbContext
    {
        public EcomDbContext(DbContextOptions<EcomDbContext> options) : base(options)
        {
        }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed Products
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = Guid.NewGuid(), Name = "Laptop", Description = "High performance laptop", AvailableQuantity = 10, Price = 1000.0, ImageUrl = "https://www.lifeofpix.com/wp-content/uploads/2018/05/p-244-ae-mint-005-1600x1169.jpg" },
                new Product { Id = Guid.NewGuid(), Name = "Smartphone", Description = "Latest model", AvailableQuantity = 20, Price = 800.0, ImageUrl = "https://media.istockphoto.com/id/1377877660/vector/realistic-mobile-phone-mockup-cellphone-app-template-isolated-stock-vector.jpg?s=612x612&w=0&k=20&c=Xw2padf6w33h9eQFFz83PL0reGEMdu1FtFsuI5G5Nf0=" },
                new Product { Id = Guid.NewGuid(), Name = "Headphones", Description = "Wireless", AvailableQuantity = 30, Price = 150.0, ImageUrl = "https://images.pexels.com/photos/3945667/pexels-photo-3945667.jpeg?cs=srgb&dl=pexels-cottonbro-studio-3945667.jpg&fm=jpg" },
                new Product { Id = Guid.NewGuid(), Name = "Camera", Description = "Digital SLR", AvailableQuantity = 5, Price = 1200.0, ImageUrl = "https://images.pexels.com/photos/274973/pexels-photo-274973.jpeg?cs=srgb&dl=pexels-pixabay-274973.jpg&fm=jpg" },
                new Product { Id = Guid.NewGuid(), Name = "Smartwatch", Description = "With fitness tracking", AvailableQuantity = 15, Price = 250.0, ImageUrl = "https://images.pexels.com/photos/393047/pexels-photo-393047.jpeg?cs=srgb&dl=pexels-alexandr-borecky-393047.jpg&fm=jpg" }
            );

            // Seed Countries
            modelBuilder.Entity<Country>().HasData(
                new Country { Id = Guid.NewGuid(), Name = "Canada", ConversionRate = 1.0, TaxRate = 0.07 },
                new Country { Id = Guid.NewGuid(), Name = "United States", ConversionRate = 1.1, TaxRate = 0.05 },
                new Country { Id = Guid.NewGuid(), Name = "United Kingdom", ConversionRate = 1.3, TaxRate = 0.1 }
            );

            // Seed Carts
            var cart1Id = Guid.NewGuid();
            var cart2Id = Guid.NewGuid();

            modelBuilder.Entity<Cart>().HasData(
                new Cart { Id = cart1Id },
                new Cart { Id = cart2Id }
            );
        }
    }
}
