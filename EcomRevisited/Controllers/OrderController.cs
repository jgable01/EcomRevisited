using EcomRevisited.Models;
using EcomRevisited.Services;
using Microsoft.AspNetCore.Mvc;

namespace EcomRevisited.Controllers
{
    public class OrderController : Controller
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        public async Task<IActionResult> Index(Guid id)
        {
            var order = await _orderService.GetOrderAsync(id);
            return View(order);
        }

        // Create a new order
        public async Task<IActionResult> Create(Order newOrder)
        {
            await _orderService.CreateOrderAsync(newOrder);
            return RedirectToAction("Index", new { id = newOrder.Id });
        }

        // List all orders
        public async Task<IActionResult> List()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return View(orders);
        }
    }
}
