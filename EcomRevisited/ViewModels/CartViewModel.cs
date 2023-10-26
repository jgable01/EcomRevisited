using System;
using System.Collections.Generic;

namespace EcomRevisited.ViewModels
{
    public class CartViewModel
    {
        public Guid Id { get; set; }
        public List<CartItemViewModel> CartItems { get; set; }
        public double TotalPrice { get; set; }
    }

    public class CartItemViewModel
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; }
    }
}
