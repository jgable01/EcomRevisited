using EcomRevisited.Data;
using EcomRevisited.Models;
using EcomRevisited.Services;

public class CartService
{
    private readonly IRepository<Cart> _cartRepository;
    private readonly IRepository<Product> _productRepository;
    private readonly IProductService _productService;  // Change this line

    public CartService(IRepository<Cart> cartRepository, IRepository<Product> productRepository, IProductService productService)  // And this line
    {
        _cartRepository = cartRepository;
        _productRepository = productRepository;
        _productService = productService;
    }

    public async Task<Cart> GetCartAsync(Guid cartId)
    {
        return await _cartRepository.GetByIdWithIncludesAsync(cart => cart.Id == cartId, "CartItems.Product");
    }

    public async Task CreateCartAsync(Cart cart)
    {
        await _cartRepository.AddAsync(cart);
    }

    public async Task<Cart> GetOrCreateCartAsync(Guid cartId)
    {
        var cart = await _cartRepository.GetByIdAsync(cartId);
        if (cart == null)
        {
            cart = new Cart { Id = cartId };
            await _cartRepository.AddAsync(cart);
        }
        return cart;
    }

    public async Task<bool> AddProductToCartAsync(Guid cartId, Guid productId, int quantity)
    {
        var product = await _productService.GetProductByIdAsync(productId);
        var cart = await _cartRepository.GetByIdAsync(cartId);

        var existingItem = cart.CartItems.FirstOrDefault(x => x.ProductId == productId);

        int newQuantity = (existingItem != null) ? existingItem.Quantity + quantity : quantity;

        var isAvailable = await _productService.IsProductAvailableAsync(productId, quantity);

        if (!isAvailable)
        {
            // Handle the case where the product is out of stock
            Console.WriteLine("The product is out of stock");
            return false;
        }

        if (cart == null)
        {
            cart = new Cart { Id = cartId };
            await _cartRepository.AddAsync(cart);
        }
        if (existingItem != null)
        {
            existingItem.Quantity += quantity;  // Update quantity
        }
        else
        {
            cart.CartItems.Add(new CartItem { ProductId = productId, Quantity = quantity });
        }

        await _cartRepository.UpdateAsync(cart);

        bool success = await _productService.UpdateProductQuantity(productId, -quantity);
        if (!success)
        {
            return false;
        }

        return true;
    }

    // Remove product from cart
    public async Task RemoveProductFromCartAsync(Guid cartId, Guid productId)
    {
        var cart = await _cartRepository.GetByIdAsync(cartId);
        var cartItem = cart.CartItems.FirstOrDefault(x => x.ProductId == productId);
        if (cartItem != null)
        {
            // Use centralized method to update product quantity
            await _productService.UpdateProductQuantity(productId, cartItem.Quantity);

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
    public async Task<double> CalculateTotalPriceAsync(Cart cart)
    {
        double totalPrice = 0;
        foreach (var item in cart.CartItems)
        {
            var product = await _productService.GetProductByIdAsync(item.ProductId);
            totalPrice += product.Price * item.Quantity;
        }
        return totalPrice;
    }

    // Increase product quantity in the cart
    public async Task<bool> IncreaseProductQuantityAsync(Guid cartId, Guid productId)
    {
        var cart = await _cartRepository.GetByIdAsync(cartId);
        var itemToUpdate = cart.CartItems.FirstOrDefault(item => item.ProductId == productId);
        var product = await _productService.GetProductByIdAsync(productId);
        var isAvailable = await _productService.IsProductAvailableAsync(productId, 1);

        if (itemToUpdate != null && product != null)
        {
            if (isAvailable)
            {
                itemToUpdate.Quantity += 1;
                await _cartRepository.UpdateAsync(cart);

                // Decrease the available quantity of the product in the inventory
                bool success = await _productService.UpdateProductQuantity(productId, -1); // Decrease by 1
                if (!success)
                {
                    throw new Exception("Failed to update product quantity");
                }

                return true;
            }
            else
            {
                Console.WriteLine("Reached the maximum available quantity for this product.");
                return false;
            }
        }
        return false;
    }


    // Decrease product quantity in the cart
    public async Task DecreaseProductQuantityAsync(Guid cartId, Guid productId)
    {
        var cart = await _cartRepository.GetByIdAsync(cartId);
        var itemToUpdate = cart.CartItems.FirstOrDefault(item => item.ProductId == productId);
        if (itemToUpdate != null)
        {
            // Decrease cart item quantity
            itemToUpdate.Quantity -= 1;

            // If quantity is zero or less, remove the item from the cart
            if (itemToUpdate.Quantity <= 0)
            {
                cart.CartItems.Remove(itemToUpdate);
            }

            await _cartRepository.UpdateAsync(cart);

            // Increase the available quantity of the product in the inventory
            bool success = await _productService.UpdateProductQuantity(productId, 1); // Increase by 1
            if (!success)
            {
                throw new Exception("Failed to update product quantity");
            }
        }
    }
}
