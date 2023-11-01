using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;  // Needed for session
using EcomRevisited.Services;
using EcomRevisited.ViewModels;
using EcomRevisited.Models;
using EcomRevisited.Services.Interfaces;

namespace EcomRevisited.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;

        public CartController(ICartService cartService, IProductService productService, IOrderService orderService)
        {
            _cartService = cartService;
            _productService = productService;
            _orderService = orderService;
        }

        private async Task<Guid> GetOrCreateCartId()
        {
            // For demonstration, using a hardcoded cart ID
            // In a real-world application, we would fetch this from a database
            Guid cartId = Guid.Parse("12345678-1234-5678-1234-567812345678");

            // Check if the cart exists
            var cart = await _cartService.GetCartAsync(cartId);
            if (cart == null)
            {
                // If the cart doesn't exist, create a new one
                cart = new Cart { Id = cartId };
                await _cartService.CreateCartAsync(cart);
            }

            return cartId;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var cartId = await GetOrCreateCartId();
            var cart = await _cartService.GetCartAsync(cartId);

            if (cart == null || cart.CartItems == null)
            {
                return View(new CartViewModel());
            }

            var cartItemViewModels = new List<CartItemViewModel>();

            foreach (var item in cart.CartItems)
            {
                var product = await _productService.GetProductByIdAsync(item.ProductId);
                if (product != null)
                {
                    var isIncreaseDisabled = (item.Quantity + 1 > product.AvailableQuantity + item.Quantity);

                    cartItemViewModels.Add(new CartItemViewModel
                    {
                        ProductId = item.ProductId,
                        ProductName = product.Name,
                        Quantity = item.Quantity,
                        Price = product.Price,
                        ImageUrl = product.ImageUrl,
                        IsIncreaseDisabled = isIncreaseDisabled
                    });
                }
            }

            // Calculate the total price of the cart
            double totalPrice = await _cartService.CalculateTotalPriceAsync(cart);

            var viewModel = new CartViewModel
            {
                Id = cart.Id,
                TotalPrice = totalPrice,
                CartItems = cartItemViewModels
            };

            return View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> AddProductToCart(Guid productId, int quantity)
        {
            var cartId = await GetOrCreateCartId();
            // Check if the product is available
            var isAvailable = await _productService.IsProductAvailableAsync(productId, quantity);
            if (!isAvailable)
            {
                TempData["OutOfStockProductId"] = productId;
                return RedirectToAction("Index", "Catalogue");
            }
            bool success = await _cartService.AddProductToCartAsync(cartId, productId, quantity);
            if (!success)
            {
                return BadRequest("Could not add product to cart. Not enough stock available.");
            }
            return RedirectToAction("Index", "Catalogue");
        }



        [HttpPost]
        public async Task<IActionResult> IncreaseProductQuantity(Guid productId)
        {
            var cartId = await GetOrCreateCartId();
            bool success = await _cartService.IncreaseProductQuantityAsync(cartId, productId);
            if (!success)
            {
                return BadRequest("Could not increase quantity. Not enough stock available.");
            }
            return RedirectToAction("Index");
        }



        [HttpPost]
        public async Task<IActionResult> RemoveProductFromCart(Guid productId)
        {
            var cartId = await GetOrCreateCartId();
            await _cartService.RemoveProductFromCartAsync(cartId, productId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DecreaseProductQuantity(Guid productId)
        {
            var cartId = await GetOrCreateCartId();
            await _cartService.DecreaseProductQuantityAsync(cartId, productId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Checkout()
        {
            var cartId = await GetOrCreateCartId();

            // Redirect to the ConfirmOrder action in OrderController with the cartId as a parameter
            return RedirectToAction("ConfirmOrder", "Order", new { cartId = cartId });
        }

    }
}
