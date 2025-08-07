using Application.Commands.Vehicle;
using common.Dtos;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class VehicleController: ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateVehicle(CreateVehicleCommand command,ICommandHandler<CreateVehicleCommand> handler, CancellationToken cancellationToken)
    {
        if (command == null)
        {
            return BadRequest("Command cannot be null");
        }

        await handler.Handle(command, cancellationToken);
        return Created();
    }

    [HttpGet]
    public async Task<IActionResult> GetVehicles(IQueryHandler<IEnumerable<VehicleDto>> handler,
        CancellationToken cancellationToken)
    {
    
        var vehicles = await handler.Handle(cancellationToken);
        return Ok(vehicles);
    }

    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> UpdateVehicle(UpdateVehicleCommand command, Guid id,
        ICommandHandler<UpdateVehicleCommand> handler, CancellationToken cancellationToken)
    {
        command.Id= id;
        await handler.Handle(command, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> DeleteVehicle(Guid id, ICommandHandler<DeleteVehicleCommand> handler,
        CancellationToken cancellationToken)
    {
        var command = new DeleteVehicleCommand { Id = id };
        await handler.Handle(command, cancellationToken);
        return NoContent();
        
    }
}