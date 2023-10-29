using EcomRevisited.Models;
using EcomRevisited.Services;
using EcomRevisited.Services.EcomRevisited.Services;
using EcomRevisited.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EcomRevisited.Controllers
{
    public class OrderController : Controller
    {
        private readonly OrderService _orderService;
        private readonly CartService _cartService;
        private readonly CountryService _countryService;

        public OrderController(OrderService orderService, CartService cartService, CountryService countryService)
        {
            _orderService = orderService;
            _cartService = cartService;
            _countryService = countryService;
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
                NumberOfItems = order.NumberOfItems,
                DestinationCountry = order.DestinationCountry,
                TotalPrice = order.TotalPrice,
                MailingCode = order.MailingCode,
                Address = order.Address
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Guid cartId, string destinationCountry, string mailingCode, string address)
        {
            var newOrderId = await _orderService.CreateOrderAsync(cartId, destinationCountry, mailingCode, address);
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

            Console.WriteLine($"Orders count: {(orders != null ? orders.Count() : 0)}");

            if (orders == null)
            {
                return View(new List<OrderViewModel>());
            }

            var viewModel = orders.Select(o => new OrderViewModel
            {
                Id = o.Id,
                NumberOfItems = o.NumberOfItems,
                DestinationCountry = o.DestinationCountry,
                TotalPrice = o.TotalPrice,
                MailingCode = o.MailingCode,
                Address = o.Address,
                ConvertedPrice = o.ConvertedPrice,
                FinalPrice = o.FinalPrice
            }).ToList();

            return View(viewModel);
        }

        public async Task<IActionResult> ConfirmOrder(Guid cartId)
        {
            var cart = await _cartService.GetCartAsync(cartId);
            if (cart == null)
            {
                return NotFound("Cart not found");
            }

            double totalPrice = await _cartService.CalculateTotalPriceAsync(cart);

            // Fetch available countries and populate the dropdown list
            var countries = await _countryService.GetAllCountriesAsync();
            ViewBag.CountryList = new SelectList(countries, "Name", "Name");

            var model = new ConfirmOrderViewModel
            {
                OrderItems = cart.CartItems.Select(item => new OrderItemViewModel
                {
                    ProductId = item.Product.Id,
                    ProductTitle = item.Product.Name,
                    Quantity = item.Quantity,
                    Price = item.Product.Price,
                }).ToList(),
                TotalPrice = totalPrice,
                ConvertedPrice = 0,
                FinalPrice = 0
            };

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> ConfirmOrder(ConfirmOrderViewModel model, Guid cartId)
        {
            if (ModelState.IsValid)
            {
                var country = await _countryService.GetCountryByNameAsync(model.DestinationCountry);
                if (country == null)
                {
                    ModelState.AddModelError("DestinationCountry", "Invalid country selected.");
                    return await ReturnToConfirmOrderView(model, cartId);
                }

                // Calculate Converted and Final Price based on selected country
                double convertedPrice = Math.Round(model.TotalPrice * country.ConversionRate, 2);
                double finalPrice = Math.Round(convertedPrice + (convertedPrice * country.TaxRate), 2);

                model.ConvertedPrice = convertedPrice;
                model.FinalPrice = finalPrice;

                var isSuccess = await _orderService.ConfirmOrderAsync(model);
                if (isSuccess)
                {
                    return RedirectToAction("List");
                }
                else
                {
                    ModelState.AddModelError("", "Could not confirm the order. Please try again.");
                }
            }
            else
            {
                ModelState.AddModelError("", "Invalid model state");
            }

            return await ReturnToConfirmOrderView(model, cartId);
        }


        private async Task<IActionResult> ReturnToConfirmOrderView(ConfirmOrderViewModel model, Guid cartId)
        {
            // Repopulate the OrderItems
            var cart = await _cartService.GetCartAsync(cartId);
            if (cart != null)
            {
                model.OrderItems = cart.CartItems.Select(item => new OrderItemViewModel
                {
                    ProductId = item.Id,
                    ProductTitle = item.Product.Name,
                    Quantity = item.Quantity,
                    Price = item.Product.Price
                }).ToList();
            }

            var countries = await _countryService.GetAllCountriesAsync();
            ViewBag.CountryList = new SelectList(countries, "Name", "Name");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetConvertedAndFinalPrice(double totalPrice, string destinationCountry)
        {
            var country = await _countryService.GetCountryByNameAsync(destinationCountry);
            if (country == null)
            {
                return NotFound();
            }

            double convertedPrice = Math.Round(totalPrice * country.ConversionRate, 2);
            double finalPrice = Math.Round(convertedPrice + (convertedPrice * country.TaxRate), 2);

            return Json(new { ConvertedPrice = convertedPrice, FinalPrice = finalPrice });
        }

    }
}
