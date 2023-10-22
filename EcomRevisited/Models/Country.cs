namespace EcomRevisited.Models
{
    public class Country
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double ConversionRate { get; set; }
        public double TaxRate { get; set; }
    }
}
