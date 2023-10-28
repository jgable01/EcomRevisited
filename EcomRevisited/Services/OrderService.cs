using EcomRevisited.Data;
using EcomRevisited.Models;
using EcomRevisited.Services.EcomRevisited.Services;
using System.Data.Entity;

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
        private readonly CartService _cartService;

        private readonly EcomDbContext _context;

        public OrderService(
            IRepository<Order> orderRepository,
            IRepository<Product> productRepository,
            IRepository<Cart> cartRepository,
            IRepository<Country> countryRepository,
            CountryService countryService,
            ProductService productService,
            CartService cartService,
            EcomDbContext context)  // Inject EcomDbContext here
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _cartRepository = cartRepository;
            _countryRepository = countryRepository;
            _countryService = countryService;
            _productService = productService;
            _cartService = cartService;
            _context = context;  // Initialize the context
        }

        public async Task<Order> GetOrderAsync(Guid orderId)
        {
            Console.WriteLine($"Fetching order with ID: {orderId}");
            var order = await _orderRepository.GetByIdWithIncludesAsync(o => o.Id == orderId, "OrderItems");
            if (order == null)
            {
                Console.WriteLine($"Order with ID: {orderId} not found.");
            }
            else
            {
                Console.WriteLine($"Found order with ID: {orderId}");
            }
            return order;
        }

        public async Task<Guid> CreateOrderAsync(Guid cartId, string destinationCountry, string mailingCode, string address)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())  // Start a new transaction
            {
                try
                {
                    // Check for null or empty parameters
                    if (cartId == Guid.Empty || string.IsNullOrEmpty(destinationCountry))
                    {
                        Console.WriteLine("Invalid parameters.");
                        // Roll back the transaction
                        transaction.Rollback();
                        return Guid.Empty;
                    }

                    // Fetch cart and validate it exists
                    var cart = await _cartRepository.GetByIdAsync(cartId);
                    cart = await _cartRepository.GetByIdWithIncludesAsync(cart => cart.Id == cartId, "CartItems.Product");
                    if (cart == null)
                    {
                        Console.WriteLine("Cart not found.");
                        // Roll back the transaction
                        transaction.Rollback();
                        return Guid.Empty;
                    }

                    // Validate the cart is not empty
                    if (!cart.CartItems.Any())
                    {
                        Console.WriteLine("Cart is empty.");
                        // Roll back the transaction
                        transaction.Rollback();
                        return Guid.Empty;
                    }

                    // Fetch country and validate it exists
                    var country = await _countryService.GetCountryByNameAsync(destinationCountry);
                    if (country == null)
                    {
                        Console.WriteLine("Destination country not found.");
                        // Roll back the transaction
                        transaction.Rollback();
                        return Guid.Empty;
                    }

                    // Initialize an Order object from the Cart
                    Order order = new()
                    {
                        OrderItems = new List<OrderItem>(),
                        DestinationCountry = destinationCountry,
                        Address = address,
                        MailingCode = mailingCode,
                        // Updated code to calculate total number of items in the cart
                        NumberOfItems = cart.CartItems.Sum(item => item.Quantity),
                    };

                    var totalPrice = await _cartService.CalculateTotalPriceAsync(cart);
                    order.TotalPrice = totalPrice;

                    // Apply country-specific rates
                    double convertedPrice = order.TotalPrice * country.ConversionRate;
                    double taxAmount = convertedPrice * country.TaxRate;
                    order.TotalPrice = convertedPrice + taxAmount;

                    await _orderRepository.AddAsync(order);
                    transaction.Commit();  // Commit the transaction if everything is successful

                    // Empty the cart
                    cart.CartItems.Clear();
                    await _cartRepository.UpdateAsync(cart);

                    Console.WriteLine($"Successfully created order with ID: {order.Id}");
                    return order.Id;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();  // Roll back the transaction in case of failure
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    return Guid.Empty;
                }
            }
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

        public async Task<bool> ConfirmOrderAsync(ConfirmOrderViewModel model)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // Create new Order entity
                    var newOrder = new Order
                    {
                        DestinationCountry = model.DestinationCountry,
                        Address = model.Address,
                        MailingCode = model.MailingCode,
                        TotalPrice = model.FinalPrice,
                        OrderItems = model.OrderItems.Select(x => new OrderItem
                        {
                            ProductId = x.ProductId,  // Use the ProductId from the ViewModel
                            Quantity = x.Quantity
                        }).ToList()
                    };

                    await _orderRepository.AddAsync(newOrder);
                    transaction.Commit();

                    return true;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }
    }
}
