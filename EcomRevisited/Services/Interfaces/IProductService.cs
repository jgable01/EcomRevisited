using EcomRevisited.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcomRevisited.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(Guid productId);
        Task<bool> IsProductAvailableAsync(Guid productId, int requiredQuantity);
        Task<bool> UpdateProductQuantity(Guid productId, int deltaQuantity);
    }
}
