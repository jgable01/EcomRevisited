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
            if (product != null && product.AvailableQuantity >= deltaQuantity)
            {
                product.AvailableQuantity += deltaQuantity;
                await _productRepository.UpdateAsync(product);
                return true;
            }
            return false;
        }



    }
}
