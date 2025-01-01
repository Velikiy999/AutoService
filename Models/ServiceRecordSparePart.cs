using AutoService.Models;

public class ServiceRecordSparePart
{
    public int ServiceRecordId { get; set; }
    public ServiceRecord ServiceRecord { get; set; }

    public int SparePartId { get; set; }
    public SparePart SparePart { get; set; }

    public int Quantity { get; set; }
}
