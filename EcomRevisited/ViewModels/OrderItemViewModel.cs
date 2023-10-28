namespace EcomRevisited.ViewModels
{
    public class OrderItemViewModel
    {
        public Guid ProductId { get; set; }  // Added ProductId
        public string ProductTitle { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}
