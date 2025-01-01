using AutoService.Models;
using System.ComponentModel.DataAnnotations;

public class ServiceRecord
{
    public int Id { get; set; }

    [Required]
    public int CarId { get; set; }

    public Car? Car { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime ServiceDate { get; set; }

    [Required]
    [StringLength(500)]
    public string Description { get; set; }

    [Required]
    public decimal Price { get; set; }

    public string Status { get; set; } = "Заплановано";

    public ICollection<ServiceRecordSparePart>? ServiceRecordSpareParts { get; set; }
}
