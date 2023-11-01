using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;
using EcomRevisited.Services;
using EcomRevisited.Models;
using EcomRevisited.Data;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace EcomRevisited.Tests
{
    [TestClass]
    public class CartServiceTests
    {
        private readonly Mock<IRepository<Cart>> _cartRepoMock = new Mock<IRepository<Cart>>();
        private readonly Mock<IRepository<Product>> _productRepoMock = new Mock<IRepository<Product>>();
        private readonly Mock<IProductService> _productServiceMock = new Mock<IProductService>();

        [TestMethod]
        public async Task AddProductToCartAsync_ProductAvailable_AddsSuccessfully()
        {
            // Arrange
            var cartId = Guid.NewGuid();
            var productId = Guid.NewGuid();

            var cartService = new CartService(_cartRepoMock.Object, _productRepoMock.Object, _productServiceMock.Object);
            var product = new Product { Id = productId, AvailableQuantity = 5 };
            var cart = new Cart { Id = cartId };

            _productServiceMock.Setup(p => p.GetProductByIdAsync(productId)).ReturnsAsync(product);
            _cartRepoMock.Setup(c => c.GetByIdAsync(cartId)).ReturnsAsync(cart);
            _productServiceMock.Setup(p => p.IsProductAvailableAsync(productId, 1)).ReturnsAsync(true);
            _productServiceMock.Setup(p => p.UpdateProductQuantity(productId, -1)).ReturnsAsync(true);  

            // Act
            var result = await cartService.AddProductToCartAsync(cartId, productId, 1);

            // Assert
            Assert.IsTrue(result);

            // Verify that the repository's UpdateAsync method was called once.
            _cartRepoMock.Verify(c => c.UpdateAsync(It.IsAny<Cart>()), Times.Once());
        }



        [TestMethod]
        public async Task AddProductToCartAsync_ProductNotAvailable_ReturnsFalse()
        {
            // Arrange
            var cartId = Guid.NewGuid();
            var productId = Guid.NewGuid();

            var cartService = new CartService(_cartRepoMock.Object, _productRepoMock.Object, _productServiceMock.Object);
            var product = new Product { Id = productId, AvailableQuantity = 0 };
            var cart = new Cart { Id = cartId };

            _productServiceMock.Setup(p => p.GetProductByIdAsync(productId)).ReturnsAsync(product);
            _cartRepoMock.Setup(c => c.GetByIdAsync(cartId)).ReturnsAsync(cart);
            _productServiceMock.Setup(p => p.IsProductAvailableAsync(productId, 1)).ReturnsAsync(false);

            // Act
            var result = await cartService.AddProductToCartAsync(cartId, productId, 1);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task RemoveProductFromCartAsync_ProductExists_RemovesSuccessfully()
        {
            // Arrange
            var cartId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var cartItem = new CartItem { ProductId = productId, Quantity = 2 };
            var cart = new Cart { Id = cartId, CartItems = new List<CartItem> { cartItem } };
            var cartService = new CartService(_cartRepoMock.Object, _productRepoMock.Object, _productServiceMock.Object);

            _cartRepoMock.Setup(c => c.GetByIdAsync(cartId)).ReturnsAsync(cart);

            // Act
            await cartService.RemoveProductFromCartAsync(cartId, productId);

            // Assert
            _cartRepoMock.Verify(c => c.UpdateAsync(It.Is<Cart>(c => !c.CartItems.Any())), Times.Once());
        }

        [TestMethod]
        public async Task RemoveProductFromCartAsync_ProductDoesNotExist_DoesNothing()
        {
            // Arrange
            var cartId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var cart = new Cart { Id = cartId, CartItems = new List<CartItem>() };
            var cartService = new CartService(_cartRepoMock.Object, _productRepoMock.Object, _productServiceMock.Object);

            _cartRepoMock.Setup(c => c.GetByIdAsync(cartId)).ReturnsAsync(cart);

            // Act
            await cartService.RemoveProductFromCartAsync(cartId, productId);

            // Assert
            _cartRepoMock.Verify(c => c.UpdateAsync(It.IsAny<Cart>()), Times.Never());
        }

        [TestMethod]
        public async Task UpdateProductQuantityAsync_ValidQuantity_UpdatesSuccessfully()
        {
            // Arrange
            var cartId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var cartItem = new CartItem { ProductId = productId, Quantity = 2 };
            var cart = new Cart { Id = cartId, CartItems = new List<CartItem> { cartItem } };
            var product = new Product { Id = productId, AvailableQuantity = 10 };
            var cartService = new CartService(_cartRepoMock.Object, _productRepoMock.Object, _productServiceMock.Object);

            _cartRepoMock.Setup(c => c.GetByIdAsync(cartId)).ReturnsAsync(cart);
            _productServiceMock.Setup(p => p.GetProductByIdAsync(productId)).ReturnsAsync(product);

            // Act
            await cartService.UpdateProductQuantityAsync(cartId, productId, 3);

            // Assert
            _cartRepoMock.Verify(c => c.UpdateAsync(It.Is<Cart>(c => c.CartItems.First().Quantity == 3)), Times.Once());
        }

        [TestMethod]
        public async Task UpdateProductQuantityAsync_InvalidQuantity_DoesNothing()
        {
            // Arrange
            var cartId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var cartItem = new CartItem { ProductId = productId, Quantity = 2 };
            var cart = new Cart { Id = cartId, CartItems = new List<CartItem> { cartItem } };
            var product = new Product { Id = productId, AvailableQuantity = 2 };
            var cartService = new CartService(_cartRepoMock.Object, _productRepoMock.Object, _productServiceMock.Object);

            _cartRepoMock.Setup(c => c.GetByIdAsync(cartId)).ReturnsAsync(cart);
            _productServiceMock.Setup(p => p.GetProductByIdAsync(productId)).ReturnsAsync(product);

            // Act
            await cartService.UpdateProductQuantityAsync(cartId, productId, 3);

            // Assert
            _cartRepoMock.Verify(c => c.UpdateAsync(It.IsAny<Cart>()), Times.Never());
        }

        [TestMethod]
        public async Task GetCartAsync_CartExists_ReturnsCart()
        {
            // Arrange
            var cartId = Guid.NewGuid();
            var cart = new Cart { Id = cartId };
            _cartRepoMock.Setup(c => c.GetByIdWithIncludesAsync(It.IsAny<Expression<Func<Cart, bool>>>(), It.IsAny<string>())).ReturnsAsync(cart);
            var cartService = new CartService(_cartRepoMock.Object, _productRepoMock.Object, _productServiceMock.Object);

            // Act
            var result = await cartService.GetCartAsync(cartId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(cartId, result.Id);
        }

        [TestMethod]
        public async Task CreateCartAsync_ValidInput_CreatesCart()
        {
            // Arrange
            var cart = new Cart { Id = Guid.NewGuid() };
            var cartService = new CartService(_cartRepoMock.Object, _productRepoMock.Object, _productServiceMock.Object);

            // Act
            await cartService.CreateCartAsync(cart);

            // Assert
            _cartRepoMock.Verify(c => c.AddAsync(cart), Times.Once());
        }

        [TestMethod]
        public async Task GetOrCreateCartAsync_CartExists_ReturnsExistingCart()
        {
            // Arrange
            var cartId = Guid.NewGuid();
            var cart = new Cart { Id = cartId };
            _cartRepoMock.Setup(c => c.GetByIdAsync(cartId)).ReturnsAsync(cart);
            var cartService = new CartService(_cartRepoMock.Object, _productRepoMock.Object, _productServiceMock.Object);

            // Act
            var result = await cartService.GetOrCreateCartAsync(cartId);

            // Assert
            Assert.AreEqual(cartId, result.Id);
            _cartRepoMock.Verify(c => c.AddAsync(It.IsAny<Cart>()), Times.Never());
        }

        [TestMethod]
        public async Task CalculateTotalPriceAsync_ValidCart_ReturnsTotalPrice()
        {
            // Arrange
            var cart = new Cart
            {
                CartItems = new List<CartItem>
                {
                    new CartItem { ProductId = Guid.NewGuid(), Quantity = 2 },
                    new CartItem { ProductId = Guid.NewGuid(), Quantity = 3 }
                }
            };
            var product1 = new Product { Id = cart.CartItems[0].ProductId, Price = 10 };
            var product2 = new Product { Id = cart.CartItems[1].ProductId, Price = 20 };
            var cartService = new CartService(_cartRepoMock.Object, _productRepoMock.Object, _productServiceMock.Object);
            _productServiceMock.Setup(p => p.GetProductByIdAsync(cart.CartItems[0].ProductId)).ReturnsAsync(product1);
            _productServiceMock.Setup(p => p.GetProductByIdAsync(cart.CartItems[1].ProductId)).ReturnsAsync(product2);

            // Act
            var totalPrice = await cartService.CalculateTotalPriceAsync(cart);

            // Assert
            Assert.AreEqual(2 * 10 + 3 * 20, totalPrice);
        }

        [TestMethod]
        public async Task IncreaseProductQuantityAsync_ValidInput_IncreasesQuantity()
        {
            // Arrange
            var cartId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var cart = new Cart { Id = cartId, CartItems = new List<CartItem> { new CartItem { ProductId = productId, Quantity = 1 } } };
            var product = new Product { Id = productId, AvailableQuantity = 5 };  

            _cartRepoMock.Setup(c => c.GetByIdAsync(cartId)).ReturnsAsync(cart);
            _productServiceMock.Setup(p => p.GetProductByIdAsync(productId)).ReturnsAsync(product);  
            _productServiceMock.Setup(p => p.IsProductAvailableAsync(productId, 1)).ReturnsAsync(true);
            _productServiceMock.Setup(p => p.UpdateProductQuantity(productId, -1)).ReturnsAsync(true);

            var cartService = new CartService(_cartRepoMock.Object, _productRepoMock.Object, _productServiceMock.Object);

            // Act
            var result = await cartService.IncreaseProductQuantityAsync(cartId, productId);

            // Assert
            Assert.IsTrue(result);
            _cartRepoMock.Verify(c => c.UpdateAsync(It.Is<Cart>(c => c.CartItems.First().Quantity == 2)), Times.Once());

            // Verify that GetProductByIdAsync was called once
            _productServiceMock.Verify(p => p.GetProductByIdAsync(productId), Times.Once());  
        }

        [TestMethod]
        public async Task DecreaseProductQuantityAsync_ValidInput_DecreasesQuantity()
        {
            // Arrange
            var cartId = Guid.NewGuid();
            var productId = Guid.NewGuid();
            var cart = new Cart { Id = cartId, CartItems = new List<CartItem> { new CartItem { ProductId = productId, Quantity = 2 } } };
            _cartRepoMock.Setup(c => c.GetByIdAsync(cartId)).ReturnsAsync(cart);
            _productServiceMock.Setup(p => p.UpdateProductQuantity(productId, 1)).ReturnsAsync(true);  
            var cartService = new CartService(_cartRepoMock.Object, _productRepoMock.Object, _productServiceMock.Object);

            // Act
            await cartService.DecreaseProductQuantityAsync(cartId, productId);

            // Assert
            _cartRepoMock.Verify(c => c.UpdateAsync(It.Is<Cart>(c => c.CartItems.First().Quantity == 1)), Times.Once());
        }

    }
}
