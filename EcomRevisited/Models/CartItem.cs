using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcomRevisited.Models
{
    public class CartItem
    {
        public Guid Id { get; set; }
        [ForeignKey("CartId")]
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
