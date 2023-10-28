using System.ComponentModel.DataAnnotations.Schema;

namespace EcomRevisited.Models
{
    public class CartItem
    {
        public Guid Id { get; set; }
        [ForeignKey("CartId")]
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }

        // Navigation property to Product
        public virtual Product Product { get; set; }
    }
}
