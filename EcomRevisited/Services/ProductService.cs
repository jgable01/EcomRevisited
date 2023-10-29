using EcomRevisited.Data;
using EcomRevisited.Models;

namespace EcomRevisited.Services
{
    public class ProductService : IProductService
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

            if (product == null)
            {
                Console.WriteLine($"Product with ID {productId} does not exist.");
                return false;
            }

            if (product.AvailableQuantity < requiredQuantity)
            {
                Console.WriteLine($"Insufficient quantity for Product ID {productId}. Required: {requiredQuantity}, Available: {product.AvailableQuantity}");
                return false;
            }

            return true;
        }

        public async Task<bool> UpdateProductQuantity(Guid productId, int deltaQuantity)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            if (product != null)
            {
                // Ensure we're not going below zero when reducing stock
                if (deltaQuantity < 0 && (product.AvailableQuantity + deltaQuantity) < 0)
                {
                    Console.WriteLine($"Not enough stock to reduce for Product ID {productId}. Available: {product.AvailableQuantity}, Trying to reduce: {Math.Abs(deltaQuantity)}");
                    return false;
                }

                product.AvailableQuantity += deltaQuantity;
                await _productRepository.UpdateAsync(product);
                return true;
            }
            return false;
        }
    }
}
