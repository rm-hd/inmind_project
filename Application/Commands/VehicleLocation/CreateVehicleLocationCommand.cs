using System.ComponentModel.DataAnnotations;

namespace Application.Commands.VehicleLocation;

public class CreateVehicleLocationCommand
{
    [Required]
    public Guid VehicleId { get; set; }
    [Required]
    [Range(-90, 90, ErrorMessage = "Latitude must be between -90 and 90")]
    public double Latitude { get; set; }
    [Required]
    [Range(-180, 180, ErrorMessage = "Longitude must be between -180 and 180")]
    public double Longitude { get; set; }
    
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}