using EcomRevisited.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using EcomRevisited.ViewModels;

namespace EcomRevisited.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllProductsAsync();

            // Convert to ViewModel
            var productViewModels = products.Select(p => new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                AvailableQuantity = p.AvailableQuantity,
                Price = p.Price,
                ImageUrl = p.ImageUrl
            }).ToList();


            var productListViewModel = new ProductListViewModel
            {
                Products = productViewModels
            };

            return View(productListViewModel);
        }

        [HttpGet("api/products/{productId}/available-quantity")]
        public async Task<IActionResult> GetAvailableQuantity(Guid productId)
        {
            var product = await _productService.GetProductByIdAsync(productId);
            return Json(product.AvailableQuantity);
        }

    }
}
