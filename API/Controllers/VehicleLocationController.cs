using Application.Commands.VehicleLocation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VehicleLocationController: ControllerBase
{
    
    
    [HttpPost("/VehicleLocation")]
    public async Task<IActionResult> SendVehicleLocation(CreateVehicleLocationCommand command,IHubContext<VehicleHub> hubContext)
    {
        await hubContext.Clients.All.SendAsync("ReceiveVehicleLocation", command);

        return Ok();
    }
}