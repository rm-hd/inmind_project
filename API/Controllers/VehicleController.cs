using Application.Commands.Vehicle;
using Application.Dtos;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class VehicleController: ControllerBase
{
    [Authorize(Roles = "fleet-user")]
    [HttpPost]
    public async Task<IActionResult> CreateVehicle(CreateVehicleCommand command,ICommandHandler<CreateVehicleCommand> handler, CancellationToken cancellationToken)
    {
        if (command is null)
        {
            return BadRequest("Command cannot be null");
        }

        await handler.Handle(command, cancellationToken);
        return Created();
    }
    [Authorize(Roles = "fleet-admin")]
    [HttpGet]
    public async Task<IActionResult> GetVehicles(IQueryHandler<IEnumerable<VehicleDto>> handler,IMemoryCache memoryCache,
        CancellationToken cancellationToken)
    {
        
         const string cacheKey = "vehicles";
         var cacheOptions = new MemoryCacheEntryOptions
         {
             AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(7),
             SlidingExpiration = TimeSpan.FromMinutes(5),
             Priority = CacheItemPriority.Normal
         };
        
        if (memoryCache.TryGetValue(cacheKey, out IEnumerable<VehicleDto> cachedProduct))
        {
            return Ok(cachedProduct);
        }
        
        var vehicles = await handler.Handle(cancellationToken);
      
        var cachedVehicles= memoryCache.Set("vehicles",vehicles,cacheOptions);;
        
        return Ok(vehicles);
    }
    [Authorize(Roles = "fleet-user")]
    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> UpdateVehicle(UpdateVehicleCommand command, Guid id,
        ICommandHandler<UpdateVehicleCommand> handler, CancellationToken cancellationToken)
    {
        command.Id= id;
        await handler.Handle(command, cancellationToken);
        return NoContent();
    }

    [Authorize(Roles = "fleet-admin")]
    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> DeleteVehicle(Guid id, ICommandHandler<DeleteVehicleCommand> handler,
        CancellationToken cancellationToken)
    {
        var command = new DeleteVehicleCommand { Id = id };
        await handler.Handle(command, cancellationToken);
        return NoContent();
        
    }
}