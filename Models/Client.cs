using System.ComponentModel.DataAnnotations;

namespace AutoService.Models
{
    public class Client
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(150)]
        public string Email { get; set; }

        [StringLength(15)]
        public string? Phone { get; set; }

        [StringLength(200)]
        public string? Address { get; set; }
        public string? UserId { get; set; }
    }
}
