using System.ComponentModel.DataAnnotations;

namespace Application.Commands;

public class DeleteDriverCommand
{
    [Required]
    public Guid Id { get; set; }
}