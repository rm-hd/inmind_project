using Microsoft.AspNetCore.SignalR;

namespace API;

public class VehicleHub:Hub
{

    public async Task SendMessage(string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", message);
    }
    
    
}