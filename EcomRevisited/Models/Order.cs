using System.ComponentModel.DataAnnotations.Schema;

namespace EcomRevisited.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        [ForeignKey("OrderId")]
        public List<OrderItem> OrderItems { get; set; }
        public string DestinationCountry { get; set; }
        public string Address { get; set; }
        public string MailingCode { get; set; }
        public double TotalPrice { get; set; }
    }
}
