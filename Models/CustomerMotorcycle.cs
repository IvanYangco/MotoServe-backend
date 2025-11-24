using backend.Models;

public partial class CustomerMotorcycle
{
    public int CustomerMotorcycleId { get; set; }
    public int CustomerId { get; set; }
    public string? Motorcycle { get; set; }  
    public string? PlateNumber { get; set; }  

    public virtual Customer? Customer { get; set; }
}
