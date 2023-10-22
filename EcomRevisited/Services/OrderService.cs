using EcomRevisited.Data;
using EcomRevisited.Models;
using EcomRevisited.Services.EcomRevisited.Services;

namespace EcomRevisited.Services
{
    public class OrderService
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Cart> _cartRepository;
        private readonly IRepository<Country> _countryRepository;
        private readonly CountryService _countryService;
        private readonly ProductService _productService;

        public OrderService(
            IRepository<Order> orderRepository,
            IRepository<Product> productRepository,
            IRepository<Cart> cartRepository,
            IRepository<Country> countryRepository,
            CountryService countryService,
            ProductService productService)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _cartRepository = cartRepository;
            _countryRepository = countryRepository;
            _countryService = countryService;
            _productService = productService;
        }

        public async Task<Order> GetOrderAsync(Guid orderId)
        {
            return await _orderRepository.GetByIdAsync(orderId);
        }

        public async Task<bool> CreateOrderAsync(Guid cartId, string destinationCountry)
        {
            // Check for null or empty parameters
            if (cartId == Guid.Empty || string.IsNullOrEmpty(destinationCountry))
            {
                Console.WriteLine("Invalid parameters.");
                return false;
            }

            // Fetch cart and validate it exists
            var cart = await _cartRepository.GetByIdAsync(cartId);
            if (cart == null)
            {
                Console.WriteLine("Cart not found.");
                return false;
            }

            // Validate the cart is not empty
            if (!cart.CartItems.Any())
            {
                Console.WriteLine("Cart is empty.");
                return false;
            }

            // Fetch country and validate it exists
            var country = await _countryService.GetCountryByNameAsync(destinationCountry);
            if (country == null)
            {
                Console.WriteLine("Destination country not found.");
                return false;
            }

            // Check for product availability
            foreach (var cartItem in cart.CartItems)
            {
                var isAvailable = await _productService.IsProductAvailableAsync(cartItem.ProductId, cartItem.Quantity);
                if (!isAvailable)
                {
                    // Handle the case where a product is out of stock
                    Console.WriteLine($"The product with ID {cartItem.ProductId} is out of stock.");
                    return false;
                }
            }

            // Initialize an Order object from the Cart
            Order order = new Order
            {
                OrderItems = new List<OrderItem>(),
                DestinationCountry = destinationCountry
            };

            // Copy items from Cart to Order and update product quantities
            foreach (var cartItem in cart.CartItems)
            {
                var orderItem = new OrderItem
                {
                    ProductId = cartItem.ProductId,
                    Quantity = cartItem.Quantity
                };
                order.OrderItems.Add(orderItem);

                // Update the available quantity for each product
                var product = await _productRepository.GetByIdAsync(cartItem.ProductId);
                product.AvailableQuantity -= cartItem.Quantity;
                await _productRepository.UpdateAsync(product);
            }

            // Calculate the initial total price based on the cart items
            double totalPrice = 0;
            foreach (var orderItem in order.OrderItems)
            {
                var product = await _productRepository.GetByIdAsync(orderItem.ProductId);
                totalPrice += product.Price * orderItem.Quantity;
            }
            order.TotalPrice = totalPrice;

            // Apply country-specific rates
            double convertedPrice = order.TotalPrice * country.ConversionRate;
            double taxAmount = convertedPrice * country.TaxRate;
            order.TotalPrice = convertedPrice + taxAmount;

            await _orderRepository.AddAsync(order);
            return true;
        }

        // Get all orders
        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _orderRepository.GetAllAsync();
        }

        // Update the total price of the order
        public async Task UpdateTotalPriceAsync(Guid orderId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            double totalPrice = 0;
            foreach (var orderItem in order.OrderItems)
            {
                var product = await _productRepository.GetByIdAsync(orderItem.ProductId);
                totalPrice += product.Price * orderItem.Quantity;
            }
            order.TotalPrice = totalPrice;
            await _orderRepository.UpdateAsync(order);
        }

        // Apply tax and conversion rate to the total price of the order
        public async Task ApplyTaxAndConversionAsync(Guid orderId, string destinationCountry)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            var country = await _countryService.GetCountryByNameAsync(destinationCountry);
            var conversionRate = country.ConversionRate;
            var taxRate = country.TaxRate;

            double convertedPrice = order.TotalPrice * conversionRate;
            double taxAmount = convertedPrice * taxRate;

            order.TotalPrice = convertedPrice + taxAmount;
            await _orderRepository.UpdateAsync(order);
        }
    }
}
