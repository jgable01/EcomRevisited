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

    }
}
