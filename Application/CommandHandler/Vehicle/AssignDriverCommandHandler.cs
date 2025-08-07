using Application.Commands;
using AutoMapper;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistance;

namespace Application.CommandHandler.Vehicle;

public class AssignDriverCommandHandler: ICommandHandler<AssignDriverCommand>
{
    private readonly Context _context;
    private readonly IMapper _mapper;
    public AssignDriverCommandHandler(Context context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task Handle(AssignDriverCommand command, CancellationToken cancellationToken)
    {
        if (command is null)
        {
            return;
        }

        var vehicle = await _context.Vehicles.FirstOrDefaultAsync(v=>v.Id.Equals(command.VehicleId),cancellationToken);
        if (vehicle is null)
        {
            return;
        }

        vehicle.DriverId = command.DriverId;
        
        _context.Vehicles.Update(vehicle);
        await _context.SaveChangesAsync(cancellationToken);
    }
}