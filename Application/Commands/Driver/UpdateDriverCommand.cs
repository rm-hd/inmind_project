using System.ComponentModel.DataAnnotations;

namespace Application.Commands;

public class UpdateDriverCommand
{
    [Required]
    public Guid Id { get; set; }
    
    [Required]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    public string LicenseNumber { get; set; } = string.Empty;
    
    [Required]
    public Guid VehicleId { get; set; }
}