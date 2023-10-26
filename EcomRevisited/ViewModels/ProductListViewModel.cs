using System.Collections.Generic;

namespace EcomRevisited.ViewModels
{
    public class ProductListViewModel
    {
        public List<ProductViewModel> Products { get; set; }
        public string SearchString { get; set; }
    }
}
