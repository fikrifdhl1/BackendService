using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendService.Models.Domain
{
    [Table("tr_order")]
    public class Transaction
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
        [Column("cart_id")]
        public int CartId { get; set; }
        [Column("total_price")]
        public float TotalPrice { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
        [Column("updated_at")] 
        public DateTime UpdatedAt { get; set; }
    }
}
