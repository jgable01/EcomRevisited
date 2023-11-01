using EcomRevisited.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace EcomRevisited.Data
{
    public interface IEcomDbContext
    {
        DbSet<Cart> Carts { get; set; }
        DbSet<Country> Countries { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<Order> Orders { get; set; }
        IDatabaseFacadeWrapper Database { get; }

        Task<IDatabaseTransaction> BeginTransactionAsync();
    }
}
