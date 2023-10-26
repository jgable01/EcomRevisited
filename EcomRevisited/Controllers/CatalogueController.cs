﻿using EcomRevisited.Services;
using EcomRevisited.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

public class CatalogueController : Controller
{
    private readonly ProductService _productService;

    public CatalogueController(ProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string searchString)
    {
        var products = await _productService.GetAllProductsAsync();
        if (!string.IsNullOrEmpty(searchString))
        {
            products = products.Where(p => p.Name.Contains(searchString) || p.Description.Contains(searchString)).ToList();
        }

        var viewModel = new ProductListViewModel
        {
            Products = products.Select(p => new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                AvailableQuantity = p.AvailableQuantity,
                Price = p.Price,
                ImageUrl = p.ImageUrl
            }).ToList(),
            SearchString = searchString
        };

        return View(viewModel);
    }
}