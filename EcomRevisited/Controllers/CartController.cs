using EcomRevisited.Services;
using Microsoft.AspNetCore.Mvc;

namespace EcomRevisited.Controllers
{
    public class CartController : Controller
    {
        private readonly CartService _cartService;

        public CartController(CartService cartService)
        {
            _cartService = cartService;
        }

        public async Task<IActionResult> Index(Guid id)
        {
            var cart = await _cartService.GetCartAsync(id);
            return View(cart);
        }

        // Add a product to cart
        public async Task<IActionResult> AddProductToCart(Guid cartId, Guid productId)
        {
            await _cartService.AddProductToCartASync(cartId, productId);
            return RedirectToAction("Index", new { id = cartId });
        }

        // Remove a product from cart
        public async Task<IActionResult> RemoveProductFromCart(Guid cartId, Guid productId)
        {
            await _cartService.RemoveProductFromCartAsync(cartId, productId);
            return RedirectToAction("Index", new { id = cartId });
        }

    }
}
