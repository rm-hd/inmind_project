namespace Domain.Entities;

public class VehicleLocation
{
    public Guid VehicleId { get; set; }
    
    public double Latitude { get; set; }
    
    public double Longitude { get; set; }
        
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}