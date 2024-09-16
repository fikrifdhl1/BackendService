using System.ComponentModel.DataAnnotations;

namespace BackendService.Models.Domain
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public string Role { get; set; }
    }
}
