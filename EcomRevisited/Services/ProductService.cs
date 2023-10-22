using EcomRevisited.Data;
using EcomRevisited.Models;

namespace EcomRevisited.Services
{
    public class ProductService
    {
        private readonly IRepository<Product> _productRepository;

        public ProductService(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task<Product> GetProductByIdAsync(Guid productId)
        {
            return await _productRepository.GetByIdAsync(productId);
        }

        public async Task<bool> IsProductAvailableAsync(Guid productId, int requiredQuantity)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            return product.AvailableQuantity >= requiredQuantity;
        }
    }
}
