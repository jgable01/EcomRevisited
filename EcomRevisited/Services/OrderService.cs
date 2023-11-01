using EcomRevisited.Data;
using EcomRevisited.Models;
using EcomRevisited.Services.Interfaces;
using System.Data.Entity;

namespace EcomRevisited.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Cart> _cartRepository;
        private readonly IRepository<Country> _countryRepository;
        private readonly ICountryService _countryService;
        private readonly IProductService _productService;
        private readonly ICartService _cartService;

        private readonly IEcomDbContext _context;

        public OrderService(
            IRepository<Order> orderRepository,
            IRepository<Product> productRepository,
            IRepository<Cart> cartRepository,
            IRepository<Country> countryRepository,
            ICountryService countryService,
            IProductService productService,
            ICartService cartService,
            IEcomDbContext context)  
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
            return await CreateOrderFromCartAsync(cartId, destinationCountry, mailingCode, address);
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
            try
            {
                Guid newOrderId = await CreateOrderFromCartAsync(model.CartId, model.DestinationCountry, model.MailingCode, model.Address);
                if (newOrderId != Guid.Empty)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }


        public async Task<Guid> CreateOrderFromCartAsync(Guid cartId, string destinationCountry, string mailingCode, string address)
        {
            if (_context == null || _context.Database == null)
            {
                throw new Exception("_context or _context.Database is null.");
            }

            if (_context == null || _context.Database == null)
            {
                throw new Exception("_context or _context.Database is null.");
            }

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // Check for null or empty parameters
                    if (cartId == Guid.Empty || string.IsNullOrEmpty(destinationCountry))
                    {
                        throw new Exception("Invalid parameters.");
                    }

                    // Fetch cart and validate it exists
                    var cart = await _cartRepository.GetByIdWithIncludesAsync(cart => cart.Id == cartId, "CartItems.Product");

                    if (cart == null || cart.CartItems == null)
                    {
                        throw new Exception("Cart or CartItems is null.");
                    }
                    foreach (var item in cart.CartItems)
                    {
                        if (item == null || item.Product == null)
                        {
                            throw new Exception("An item or item.Product is null.");
                        }
                    }

                    if (cart == null)
                    {
                        throw new Exception("Cart not found.");
                    }

                    // Validate the cart is not empty
                    if (!cart.CartItems.Any())
                    {
                        throw new Exception("Cart is empty.");
                    }

                    // Fetch country and validate it exists
                    var country = await _countryService.GetCountryByNameAsync(destinationCountry);
                    if (country == null)
                    {
                        throw new Exception("Destination country not found.");
                    }

                    // Initialize an Order object from the Cart
                    Order order = new Order
                    {
                        OrderItems = cart.CartItems.Select(ci => new OrderItem
                        {
                            ProductId = ci.ProductId,
                            Quantity = ci.Quantity
                        }).ToList(),
                        DestinationCountry = destinationCountry,
                        Address = address,
                        MailingCode = mailingCode,
                        NumberOfItems = cart.CartItems.Sum(item => item.Quantity)
                    };

                    // Calculate total price based on cart items
                    var totalPrice = cart.CartItems.Sum(item => item.Product.Price * item.Quantity);
                    order.TotalPrice = totalPrice;

                    // Apply country-specific rates
                    double convertedPrice = order.TotalPrice * country.ConversionRate;
                    double taxAmount = convertedPrice * country.TaxRate;

                    // Update Converted and Final Price
                    order.ConvertedPrice = convertedPrice;
                    order.FinalPrice = convertedPrice + taxAmount;

                    await _orderRepository.AddAsync(order);
                    transaction.Commit();

                    // Empty the cart
                    cart.CartItems.Clear();
                    await _cartRepository.UpdateAsync(cart);

                    return order.Id;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw; // Re-throw the caught exception
                }
            }
        }

    }
}
