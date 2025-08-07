using System.ComponentModel.DataAnnotations;

namespace Application.Commands.Vehicle;

public class CreateVehicleCommand
{
    [Required]
    public string PlateNumber { get; set; } = string.Empty;
    
    [Required]
    public string Model { get; set; } = string.Empty;
    
    [Required]
    public string Status { get; set; } = string.Empty;
}