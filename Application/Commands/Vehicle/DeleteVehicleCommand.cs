using System.ComponentModel.DataAnnotations;

namespace Application.Commands.Vehicle;

public class DeleteVehicleCommand
{
    [Required]
    public Guid Id { get; set; }
    
}