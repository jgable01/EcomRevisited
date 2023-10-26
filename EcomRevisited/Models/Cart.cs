using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EcomRevisited.Models
{
    public class Cart
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("CartId")]
        public List<CartItem>? CartItems { get; set; } = new List<CartItem>();
    }
}
