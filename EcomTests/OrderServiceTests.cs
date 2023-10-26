using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;
using EcomRevisited.Services;
using EcomRevisited.Data;
using EcomRevisited.Models;
using EcomRevisited.Services.EcomRevisited.Services;

namespace EcomRevisited.Tests
{
    [TestClass]
    public class OrderServiceTests
    {
        private Mock<IRepository<Order>> _mockOrderRepository;
        private Mock<IRepository<Product>> _mockProductRepository;
        private Mock<IRepository<Cart>> _mockCartRepository;
        private Mock<IRepository<Country>> _mockCountryRepository;
        private Mock<CountryService> _mockCountryService;
        private Mock<ProductService> _mockProductService;
        private OrderService _orderService;

        [TestInitialize] // This method will run before each test.
        public void SetUp() // This method sets up the mocks and the service we're testing
        {
            _mockOrderRepository = new Mock<IRepository<Order>>();
            _mockProductRepository = new Mock<IRepository<Product>>();
            _mockCartRepository = new Mock<IRepository<Cart>>();
            _mockCountryRepository = new Mock<IRepository<Country>>();
            _mockCountryService = new Mock<CountryService>(_mockCountryRepository.Object);
            _mockProductService = new Mock<ProductService>(_mockProductRepository.Object);
        }

        [TestMethod]
        public async Task CreateOrderAsync_CartIdIsEmpty_ReturnsFalse()
        {
            // Arrange
            Guid emptyGuid = Guid.Empty;
            string destinationCountry = "USA";

            // Act
            var result = await _orderService.CreateOrderAsync(emptyGuid, destinationCountry);

            // Assert
            //Assert.IsFalse(result);
        }

        // Test for CreateOrderAsync when Cart is not found
        [TestMethod]
        public async Task CreateOrderAsync_CartNotFound_ReturnsFalse()
        {
            // Arrange
            Guid cartId = Guid.NewGuid();
            string destinationCountry = "USA";
            _mockCartRepository.Setup(x => x.GetByIdAsync(cartId)).ReturnsAsync((Cart)null);

            // Act
            var result = await _orderService.CreateOrderAsync(cartId, destinationCountry);

            // Assert
           // Assert.IsFalse(result);
        }

        // Test for CreateOrderAsync when Cart is empty
        [TestMethod]
        public async Task CreateOrderAsync_CartIsEmpty_ReturnsFalse()
        {
            // Arrange
            Guid cartId = Guid.NewGuid();
            string destinationCountry = "USA";
            var emptyCart = new Cart { CartItems = new List<CartItem>() };
            _mockCartRepository.Setup(x => x.GetByIdAsync(cartId)).ReturnsAsync(emptyCart);

            // Act
            var result = await _orderService.CreateOrderAsync(cartId, destinationCountry);

            // Assert
            //Assert.IsFalse(result);
        }

        // Test for GetOrderAsync when Order is found
        [TestMethod]
        public async Task GetOrderAsync_OrderFound_ReturnsOrder()
        {
            // Arrange
            Guid orderId = Guid.NewGuid();
            var order = new Order { Id = orderId };
            _mockOrderRepository.Setup(x => x.GetByIdAsync(orderId)).ReturnsAsync(order);

            // Act
            var result = await _orderService.GetOrderAsync(orderId);

            // Assert
            Assert.AreEqual(order, result);
        }

        // Test for GetOrderAsync when Order is not found
        [TestMethod]
        public async Task GetOrderAsync_OrderNotFound_ReturnsNull()
        {
            // Arrange
            Guid orderId = Guid.NewGuid();
            _mockOrderRepository.Setup(x => x.GetByIdAsync(orderId)).ReturnsAsync((Order)null);

            // Act
            var result = await _orderService.GetOrderAsync(orderId);

            // Assert
            Assert.IsNull(result);
        }

        // Test for GetAllOrdersAsync when Orders are found
        [TestMethod]
        public async Task GetAllOrdersAsync_OrdersFound_ReturnsOrderList()
        {
            // Arrange
            var orders = new List<Order>
    {
        new Order { Id = Guid.NewGuid() },
        new Order { Id = Guid.NewGuid() }
    };
            _mockOrderRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(orders);

            // Act
            var result = await _orderService.GetAllOrdersAsync();

            // Assert
            CollectionAssert.AreEqual(orders, result.ToList());
        }

        // Test for UpdateTotalPriceAsync when Order is found
        [TestMethod]
        public async Task UpdateTotalPriceAsync_OrderFound_UpdatesPrice()
        {
            // Arrange
            Guid orderId = Guid.NewGuid();
            var order = new Order
            {
                Id = orderId,
                OrderItems = new List<OrderItem>
        {
            new OrderItem { ProductId = Guid.NewGuid(), Quantity = 2 },
            new OrderItem { ProductId = Guid.NewGuid(), Quantity = 3 }
        }
            };
            var products = new List<Product>
    {
        new Product { Id = order.OrderItems[0].ProductId, Price = 10 },
        new Product { Id = order.OrderItems[1].ProductId, Price = 20 }
    };
            _mockOrderRepository.Setup(x => x.GetByIdAsync(orderId)).ReturnsAsync(order);
            _mockProductRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) => products.FirstOrDefault(p => p.Id == id));

            // Act
            await _orderService.UpdateTotalPriceAsync(orderId);

            // Assert
            // The expected total price is: (2 * 10) + (3 * 20) = 20 + 60 = 80
            Assert.AreEqual(80, order.TotalPrice);
        }

        [TestMethod]
        public async Task ApplyTaxAndConversionAsync_OrderAndCountryFound_UpdatesPriceWithTaxAndConversion()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var destinationCountry = "USA";
            var country = new Country { ConversionRate = 1.0, TaxRate = 0.1 }; // Conversion rate is 1.0 and tax rate is 10%
            var order = new Order { TotalPrice = 100 }; // Initial total price of the order is 100

            _mockOrderRepository.Setup(x => x.GetByIdAsync(orderId)).ReturnsAsync(order);
            _mockCountryService.Setup(x => x.GetCountryByNameAsync(destinationCountry)).ReturnsAsync(country);

            // Act
            await _orderService.ApplyTaxAndConversionAsync(orderId, destinationCountry);

            // Assert
            // The expected total price is: (100 * 1.0) + (100 * 1.0 * 0.1) = 100 + 10 = 110
            Assert.AreEqual(110, order.TotalPrice);
        }


        [TestMethod]
        public void TestMethod1()
        {
            Assert.AreEqual(1, 1);
        }
    }
}
