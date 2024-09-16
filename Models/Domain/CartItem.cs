using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendService.Models.Domain
{
    [Table("cart_item")]
    public class CartItem
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("cart_id")]
        public int CartId { get; set; }

        [Column("product_id")]
        public int ProductId { get; set; }

        [Column("quantity")]
        public int Quantity { get; set; }

        [Column("price_unit")]
        public float PriceUnit { get; set; }

        [Column("total_price")]
        public float TotalPrice { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }

        public Cart Cart { get; set; }
    }
}
