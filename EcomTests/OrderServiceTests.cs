using EcomRevisited.Data;
using EcomRevisited.Models;
using EcomRevisited.Services.Interfaces;
using EcomRevisited.Services;
using EcomRevisited.Tests;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Storage;
using System.Net;
using System.Diagnostics.Metrics;

namespace EcomRevisited.Tests
{
    [TestClass]
    public class OrderServiceTests
    {
        private Mock<IRepository<Order>> _orderRepoMock = new Mock<IRepository<Order>>();
        private Mock<IRepository<Product>> _productRepoMock = new Mock<IRepository<Product>>();
        private Mock<IRepository<Cart>> _cartRepoMock = new Mock<IRepository<Cart>>();
        private Mock<IRepository<Country>> _countryRepoMock = new Mock<IRepository<Country>>();
        private Mock<ICountryService> _countryServiceMock = new Mock<ICountryService>();
        private Mock<IProductService> _productServiceMock = new Mock<IProductService>();
        private Mock<ICartService> _cartServiceMock = new Mock<ICartService>();
        private Mock<IEcomDbContext> _contextMock = new Mock<IEcomDbContext>();
        private Mock<IDatabaseFacadeWrapper> _databaseFacadeWrapperMock = new Mock<IDatabaseFacadeWrapper>();

        [TestMethod]
        public async Task GetOrderAsync_OrderExists_ReturnsOrder()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var expectedOrder = new Order { Id = orderId };
            _orderRepoMock.Setup(o => o.GetByIdWithIncludesAsync(It.IsAny<Expression<Func<Order, bool>>>(), It.IsAny<string>()))
                .ReturnsAsync(expectedOrder);

            // Using interfaces in the OrderService constructor
            var service = new OrderService(
                _orderRepoMock.Object,
                _productRepoMock.Object,
                _cartRepoMock.Object,
                _countryRepoMock.Object,
                _countryServiceMock.Object,
                _productServiceMock.Object,
                _cartServiceMock.Object,
                _contextMock.Object
            );


            // Act
            var actualOrder = await service.GetOrderAsync(orderId);

            // Assert
            Assert.AreEqual(expectedOrder, actualOrder);
        }

        [TestMethod]
        public async Task GetOrderAsync_OrderDoesNotExist_ReturnsNull()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            _orderRepoMock.Setup(o => o.GetByIdWithIncludesAsync(It.IsAny<Expression<Func<Order, bool>>>(), It.IsAny<string>()))
                .ReturnsAsync((Order)null);

            var service = new OrderService(_orderRepoMock.Object, _productRepoMock.Object, _cartRepoMock.Object, _countryRepoMock.Object,
                _countryServiceMock.Object, _productServiceMock.Object, _cartServiceMock.Object, _contextMock.Object);

            // Act
            var actualOrder = await service.GetOrderAsync(orderId);

            // Assert
            Assert.IsNull(actualOrder);
        }

        [TestMethod]
        public async Task CreateOrderAsync_ValidInput_CreatesOrder()
        {
            // Arrange
            var cartId = Guid.NewGuid();
            var destinationCountry = "USA";
            var mailingCode = "12345";
            var address = "123 Main St";
            var newOrder = new Order { Id = Guid.NewGuid() };

            _cartRepoMock.Setup(c => c.GetByIdWithIncludesAsync(It.IsAny<Expression<Func<Cart, bool>>>(), It.IsAny<string>()))
                        .ReturnsAsync(new Cart
                        {
                            Id = cartId,
                            CartItems = new List<CartItem>
                            {
                    new CartItem
                    {
                        Product = new Product
                        {
                            Id = Guid.NewGuid(),
                            Price = 10
                        },
                        Quantity = 1 
                    },
                                
                            }
                        });


            _countryServiceMock.Setup(s => s.GetCountryByNameAsync(destinationCountry))
                               .ReturnsAsync(new Country());
            _orderRepoMock.Setup(o => o.AddAsync(It.IsAny<Order>()))
                          .Callback<Order>(order => order.Id = Guid.NewGuid())
                          .Returns(Task.CompletedTask);


            // Set up the DatabaseFacadeWrapper mock
            var mockTransaction = new Mock<IDbContextTransaction>();
            _databaseFacadeWrapperMock.Setup(d => d.BeginTransactionAsync()).ReturnsAsync(mockTransaction.Object);

            // Inject the DatabaseFacadeWrapper mock into the context mock
            _contextMock.Setup(c => c.Database).Returns(_databaseFacadeWrapperMock.Object);

            var service = new OrderService(
                _orderRepoMock.Object,
                _productRepoMock.Object,
                _cartRepoMock.Object,
                _countryRepoMock.Object,
                _countryServiceMock.Object,
                _productServiceMock.Object,
                _cartServiceMock.Object,
                _contextMock.Object
            );

            // Act
            var orderId = await service.CreateOrderAsync(cartId, destinationCountry, mailingCode, address);

            // Assert
            Assert.AreNotEqual(Guid.Empty, orderId);
            mockTransaction.Verify(t => t.Commit(), Times.Once); // Verify that Commit was called once
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task CreateOrderAsync_InvalidInput_ThrowsException()
        {
            // Arrange
            var cartId = Guid.NewGuid();
            var destinationCountry = "";
            var mailingCode = "12345";
            var address = "123 Main St";

            // Simulate conditions that would result in an exception
            _cartRepoMock.Setup(c => c.GetByIdWithIncludesAsync(It.IsAny<Expression<Func<Cart, bool>>>(), It.IsAny<string>()))
                         .ReturnsAsync((Cart)null); // Simulate a cart not being found

            _countryServiceMock.Setup(cs => cs.GetCountryByNameAsync(It.IsAny<string>()))
                               .ReturnsAsync((Country)null); // Simulate country not being found

            var service = new OrderService(
                _orderRepoMock.Object, _productRepoMock.Object, _cartRepoMock.Object,
                _countryRepoMock.Object, _countryServiceMock.Object, _productServiceMock.Object,
                _cartServiceMock.Object, _contextMock.Object);

            // Act
            await service.CreateOrderAsync(cartId, destinationCountry, mailingCode, address);

            // Assert
            // Expected exception, so no further assertions are needed
        }

        [TestMethod]
        public async Task GetAllOrdersAsync_ReturnsAllOrders()
        {
            // Arrange
            var orders = new List<Order>
    {
        new Order { Id = Guid.NewGuid() },
        new Order { Id = Guid.NewGuid() }
    };
            _orderRepoMock.Setup(o => o.GetAllAsync()).ReturnsAsync(orders);
            var service = new OrderService(
                _orderRepoMock.Object, _productRepoMock.Object, _cartRepoMock.Object,
                _countryRepoMock.Object, _countryServiceMock.Object, _productServiceMock.Object,
                _cartServiceMock.Object, _contextMock.Object);

            // Act
            var result = await service.GetAllOrdersAsync();

            // Assert
            Assert.AreEqual(orders.Count, result.Count());
        }

        [TestMethod]
        public async Task UpdateTotalPriceAsync_UpdatesTotalPrice()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var expectedTotalPrice = 100.0;
            var order = new Order { Id = orderId, OrderItems = new List<OrderItem> { new OrderItem { ProductId = Guid.NewGuid(), Quantity = 2 } } };
            var product = new Product { Id = Guid.NewGuid(), Price = 50.0 };

            _orderRepoMock.Setup(o => o.GetByIdAsync(orderId)).ReturnsAsync(order);
            _productRepoMock.Setup(p => p.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(product);

            var service = new OrderService(
                _orderRepoMock.Object, _productRepoMock.Object, _cartRepoMock.Object,
                _countryRepoMock.Object, _countryServiceMock.Object, _productServiceMock.Object,
                _cartServiceMock.Object, _contextMock.Object);

            // Act
            await service.UpdateTotalPriceAsync(orderId);

            // Assert
            Assert.AreEqual(expectedTotalPrice, order.TotalPrice);
        }

        [TestMethod]
        public async Task ApplyTaxAndConversionAsync_AppliesTaxAndConversion()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var destinationCountry = "United States";
            var order = new Order { Id = orderId, TotalPrice = 100.0 };
            var country = new Country { ConversionRate = 1.1, TaxRate = 0.05 };

            _orderRepoMock.Setup(o => o.GetByIdAsync(orderId)).ReturnsAsync(order);
            _countryServiceMock.Setup(c => c.GetCountryByNameAsync(destinationCountry)).ReturnsAsync(country);

            var service = new OrderService(
                _orderRepoMock.Object, _productRepoMock.Object, _cartRepoMock.Object,
                _countryRepoMock.Object, _countryServiceMock.Object, _productServiceMock.Object,
                _cartServiceMock.Object, _contextMock.Object);

            // Act
            await service.ApplyTaxAndConversionAsync(orderId, destinationCountry);

            var taxRate = country.TaxRate;

            double convertedPrice = order.TotalPrice * country.ConversionRate;
            double taxAmount = convertedPrice * taxRate;

            var expectedPrice = order.TotalPrice = convertedPrice + taxAmount;

            // Assert
            Assert.AreEqual(expectedPrice, order.TotalPrice);
        }


        [TestMethod]
        public async Task ConfirmOrderAsync_ValidInput_ConfirmsOrder()
        {
            // Arrange
            var destinationCountry = "USA";
            var country = new Country { ConversionRate = 1.1, TaxRate = 0.05 };
            var model = new ConfirmOrderViewModel { CartId = Guid.NewGuid(), DestinationCountry = "USA", MailingCode = "12345", Address = "123 Main St" };

            _orderRepoMock.Setup(o => o.AddAsync(It.IsAny<Order>()))
                          .Callback<Order>(order => order.Id = Guid.NewGuid())
                          .Returns(Task.CompletedTask);
            _contextMock.Setup(c => c.Database).Returns(_databaseFacadeWrapperMock.Object);
            _cartRepoMock.Setup(c => c.GetByIdWithIncludesAsync(It.IsAny<Expression<Func<Cart, bool>>>(), It.IsAny<string>()))
                        .ReturnsAsync(new Cart
                        {
                            Id = model.CartId,
                            CartItems = new List<CartItem>
                            {
                    new CartItem
                    {
                        Product = new Product
                        {
                            Id = Guid.NewGuid(),
                            Price = 10
                        },
                        Quantity = 1
                    },
                            }
                        });

            _countryServiceMock.Setup(c => c.GetCountryByNameAsync(destinationCountry)).ReturnsAsync(country);

            var mockTransaction = new Mock<IDbContextTransaction>();
            _databaseFacadeWrapperMock.Setup(d => d.BeginTransactionAsync()).ReturnsAsync(mockTransaction.Object);

            // Inject the DatabaseFacadeWrapper mock into the context mock
            _contextMock.Setup(c => c.Database).Returns(_databaseFacadeWrapperMock.Object);

            var service = new OrderService(
                _orderRepoMock.Object,
                _productRepoMock.Object,
                _cartRepoMock.Object,
                _countryRepoMock.Object,
                _countryServiceMock.Object,
                _productServiceMock.Object,
                _cartServiceMock.Object,
                _contextMock.Object
            );

            // Act
            bool isConfirmed = await service.ConfirmOrderAsync(model);

            // Assert
            Assert.IsTrue(isConfirmed);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task CreateOrderFromCartAsync_InvalidCart_ThrowsException()
        {
            // Arrange
            var cartId = Guid.NewGuid();
            var destinationCountry = "USA";
            var mailingCode = "12345";
            var address = "123 Main St";

            _cartRepoMock.Setup(c => c.GetByIdWithIncludesAsync(It.IsAny<Expression<Func<Cart, bool>>>(), It.IsAny<string>()))
                        .ReturnsAsync((Cart)null);

            var service = new OrderService(
                _orderRepoMock.Object, _productRepoMock.Object, _cartRepoMock.Object,
                _countryRepoMock.Object, _countryServiceMock.Object, _productServiceMock.Object,
                _cartServiceMock.Object, _contextMock.Object);

            // Act
            await service.CreateOrderFromCartAsync(cartId, destinationCountry, mailingCode, address);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task CreateOrderFromCartAsync_InvalidCountry_ThrowsException()
        {
            // Arrange
            var cartId = Guid.NewGuid();
            var destinationCountry = "InvalidCountry";
            var mailingCode = "12345";
            var address = "123 Main St";

            _countryServiceMock.Setup(c => c.GetCountryByNameAsync(destinationCountry)).ReturnsAsync((Country)null);

            var service = new OrderService(
                _orderRepoMock.Object, _productRepoMock.Object, _cartRepoMock.Object,
                _countryRepoMock.Object, _countryServiceMock.Object, _productServiceMock.Object,
                _cartServiceMock.Object, _contextMock.Object);

            // Act
            await service.CreateOrderFromCartAsync(cartId, destinationCountry, mailingCode, address);
        }

    }
}
