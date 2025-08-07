using System.ComponentModel.DataAnnotations;

namespace Application.Commands;

public class CreateDriverCommand
{
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string LicenseNumber { get; set; } = string.Empty;

}