using EcomRevisited.Data;
using EcomRevisited.Models;

namespace EcomRevisited.Services
{
    public class CartService
    {
        private readonly IRepository<Cart> _cartRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly ProductService _productService;

        public CartService(IRepository<Cart> cartRepository, IRepository<Product> productRepository, ProductService productService)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
            _productService = productService;
        }

        public async Task<Cart> GetCartAsync(Guid cartId)
        {
            return await _cartRepository.GetByIdAsync(cartId);
        }

        // Add product to cart
        public async Task<bool> AddProductToCartASync(Guid cartId, Guid productId, int quantity)
        {
            var isAvailable = await _productService.IsProductAvailableAsync(productId, quantity);
            if (!isAvailable)
            {
                // Handle the case where the product is out of stock
                Console.WriteLine("The product is out of stock");
                return false;
            }

            var cart = await _cartRepository.GetByIdAsync(cartId);
            var product = await _productService.GetProductByIdAsync(productId);

            cart.CartItems.Add(new CartItem { ProductId = productId, Quantity = quantity });
            await _cartRepository.UpdateAsync(cart);

            product.AvailableQuantity -= quantity;
            await _productRepository.UpdateAsync(product);

            return true;
        }


        // Remove product from cart
        public async Task RemoveProductFromCartAsync(Guid cartId, Guid productId)
        {
            var cart = await _cartRepository.GetByIdAsync(cartId);
            var cartItem = cart.CartItems.FirstOrDefault(x => x.ProductId == productId);
            if (cartItem != null)
            {
                cart.CartItems.Remove(cartItem);
                await _cartRepository.UpdateAsync(cart);
            }
        }

        // Update product quantity in the cart
        public async Task UpdateProductQuantityAsync(Guid cartId, Guid productId, int newQuantity)
        {
            var cart = await _cartRepository.GetByIdAsync(cartId);
            var itemToUpdate = cart.CartItems.FirstOrDefault(item => item.ProductId == productId);
            if (itemToUpdate != null)
            {
                var product = await _productService.GetProductByIdAsync(productId);
                if (newQuantity <= product.AvailableQuantity)
                {
                    itemToUpdate.Quantity = newQuantity;
                    await _cartRepository.UpdateAsync(cart);
                }
                else
                {
                    // Handle the case where the new quantity exceeds the available quantity
                    Console.WriteLine("The new quantity exceeds the available quantity");
                }
            }
        }

        // Calculate the total price of the cart
        public double CalculateTotalPrice(Cart cart)
        {
            double totalPrice = 0;
            foreach (var item in cart.CartItems)
            {
                var product = _productService.GetProductByIdAsync(item.ProductId).Result;
                totalPrice += product.Price * item.Quantity;
            }
            return totalPrice;
        }
    }
}
