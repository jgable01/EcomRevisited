using EcomRevisited.Data;
using EcomRevisited.Models;
using EcomRevisited.Services;

namespace EcomRevisited.Tests
{
    [TestClass]
    public class ProductServiceTests
    {
        private Mock<IRepository<Product>> _productRepoMock = new Mock<IRepository<Product>>();

        [TestMethod]
        public async Task GetAllProductsAsync_ReturnsAllProducts()
        {
            // Arrange
            var expectedProducts = new List<Product>
            {
                new Product { Id = Guid.NewGuid() },
                new Product { Id = Guid.NewGuid() }
            };
            _productRepoMock.Setup(p => p.GetAllAsync()).ReturnsAsync(expectedProducts);

            var service = new ProductService(_productRepoMock.Object);

            // Act
            var actualProducts = await service.GetAllProductsAsync();

            // Assert
            Assert.AreEqual(expectedProducts.Count, actualProducts.Count());
        }

        [TestMethod]
        public async Task GetProductByIdAsync_ProductExists_ReturnsProduct()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var expectedProduct = new Product { Id = productId };
            _productRepoMock.Setup(p => p.GetByIdAsync(productId)).ReturnsAsync(expectedProduct);

            var service = new ProductService(_productRepoMock.Object);

            // Act
            var actualProduct = await service.GetProductByIdAsync(productId);

            // Assert
            Assert.AreEqual(expectedProduct, actualProduct);
        }

        [TestMethod]
        public async Task IsProductAvailableAsync_ProductExistsAndAvailable_ReturnsTrue()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var product = new Product { Id = productId, AvailableQuantity = 5 };
            _productRepoMock.Setup(p => p.GetByIdAsync(productId)).ReturnsAsync(product);

            var service = new ProductService(_productRepoMock.Object);

            // Act
            var isAvailable = await service.IsProductAvailableAsync(productId, 3);

            // Assert
            Assert.IsTrue(isAvailable);
        }

        [TestMethod]
        public async Task UpdateProductQuantity_PositiveDelta_UpdatesQuantity()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var product = new Product { Id = productId, AvailableQuantity = 5 };
            _productRepoMock.Setup(p => p.GetByIdAsync(productId)).ReturnsAsync(product);

            var service = new ProductService(_productRepoMock.Object);

            // Act
            var isUpdated = await service.UpdateProductQuantity(productId, 3);

            // Assert
            Assert.IsTrue(isUpdated);
            Assert.AreEqual(8, product.AvailableQuantity);
        }

        [TestMethod]
        public async Task GetProductByIdAsync_ProductDoesNotExist_ReturnsNull()
        {
            // Arrange
            var productId = Guid.NewGuid();
            _productRepoMock.Setup(p => p.GetByIdAsync(productId)).ReturnsAsync((Product)null);

            var service = new ProductService(_productRepoMock.Object);

            // Act
            var actualProduct = await service.GetProductByIdAsync(productId);

            // Assert
            Assert.IsNull(actualProduct);
        }

        [TestMethod]
        public async Task IsProductAvailableAsync_ProductDoesNotExist_ReturnsFalse()
        {
            // Arrange
            var productId = Guid.NewGuid();
            _productRepoMock.Setup(p => p.GetByIdAsync(productId)).ReturnsAsync((Product)null);

            var service = new ProductService(_productRepoMock.Object);

            // Act
            var isAvailable = await service.IsProductAvailableAsync(productId, 1);

            // Assert
            Assert.IsFalse(isAvailable);
        }

        [TestMethod]
        public async Task IsProductAvailableAsync_InsufficientQuantity_ReturnsFalse()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var product = new Product { Id = productId, AvailableQuantity = 1 };
            _productRepoMock.Setup(p => p.GetByIdAsync(productId)).ReturnsAsync(product);

            var service = new ProductService(_productRepoMock.Object);

            // Act
            var isAvailable = await service.IsProductAvailableAsync(productId, 5);

            // Assert
            Assert.IsFalse(isAvailable);
        }

        [TestMethod]
        public async Task UpdateProductQuantity_NegativeDelta_ReduceBelowZero_ReturnsFalse()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var product = new Product { Id = productId, AvailableQuantity = 2 };
            _productRepoMock.Setup(p => p.GetByIdAsync(productId)).ReturnsAsync(product);

            var service = new ProductService(_productRepoMock.Object);

            // Act
            var isUpdated = await service.UpdateProductQuantity(productId, -3);

            // Assert
            Assert.IsFalse(isUpdated);
            Assert.AreEqual(2, product.AvailableQuantity);  // Quantity should remain the same
        }

    }
}

