using EcomRevisited.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using EcomRevisited.ViewModels;

namespace EcomRevisited.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
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
    }
}
