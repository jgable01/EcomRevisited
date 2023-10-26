using EcomRevisited.Models;
using EcomRevisited.Services;
using EcomRevisited.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EcomRevisited.Controllers
{
    public class OrderController : Controller
    {
        private readonly OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(Guid id)
        {
            var order = await _orderService.GetOrderAsync(id);

            if (order == null)
            {
                return NotFound("Order not found");
            }

            var viewModel = new OrderViewModel
            {
                Id = order.Id,
                NumberOfItems = order.OrderItems?.Count ?? 0,  
                DestinationCountry = order.DestinationCountry,
                TotalPrice = order.TotalPrice
            };

            return View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Create(Guid cartId, string destinationCountry)
        {
            var newOrderId = await _orderService.CreateOrderAsync(cartId, destinationCountry);
            if (newOrderId == Guid.Empty) 
            {
                return BadRequest();
            }
            return RedirectToAction("Index", new { id = newOrderId });
        }


        [HttpGet]
        public async Task<IActionResult> List()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            var viewModel = orders.Select(o => new OrderViewModel
            {
                Id = o.Id,
                NumberOfItems = o.OrderItems.Count,
                DestinationCountry = o.DestinationCountry,
                TotalPrice = o.TotalPrice
            }).ToList();
            return View(viewModel);
        }
    }
}
