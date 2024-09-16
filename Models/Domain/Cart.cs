using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendService.Models.Domain
{
    [Table("cart")]
    public class Cart
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("total_amount")]
        public float TotalAmount { get; set; }

        [Column("items")]
        public IEnumerable<CartItem> Items { get; set; }

        [Column("status")]
        public int Status { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }

}
