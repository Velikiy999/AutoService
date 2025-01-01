using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoService.Models
{
    public class Car
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Make { get; set; }

        [Required]
        [StringLength(50)]
        public string Model { get; set; }

        [Required]
        [Range(1886, 2100)]
        public int Year { get; set; }

        [Required]
        [StringLength(20)]
        public string RegistrationNumber { get; set; }

        [StringLength(200)]
        public string Description { get; set; } 

        [Required]
        public int ClientId { get; set; }

        [ForeignKey("ClientId")]
        public Client? Client { get; set; }
    }
}
