namespace EcomRevisited.ViewModels
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int AvailableQuantity { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; }
    }
}
