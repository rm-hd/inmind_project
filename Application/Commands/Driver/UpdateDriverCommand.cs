using System.ComponentModel.DataAnnotations;

namespace Application.Commands.Driver;

public class UpdateDriverCommand
{
    
    public Guid Id { get; set; }
    
    [Required]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    public string LicenseNumber { get; set; } = string.Empty;
    
    [Required]
    public Guid VehicleId { get; set; }
}