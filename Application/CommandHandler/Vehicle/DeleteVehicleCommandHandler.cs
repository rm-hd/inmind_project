using Application.Commands.Vehicle;
using AutoMapper;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistance;

namespace Application.CommandHandler.Vehicle;

public class DeleteVehicleCommandHandler: ICommandHandler<DeleteVehicleCommand>
{
    private readonly Context _context;
    private readonly IMapper _mapper;
    public DeleteVehicleCommandHandler(Context context, IMapper mapper)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task Handle(DeleteVehicleCommand command, CancellationToken cancellationToken)
    {
        if (command is null)
        {
            return;
        }

        var vehicle = await _context.Vehicles.FirstOrDefaultAsync(d => d.Id.Equals(command.Id), cancellationToken);

        if (vehicle is null)
        {
            return;
        }

        var driver = await _context.Drivers.Where(d => d.VehicleId.Equals(command.Id)).FirstOrDefaultAsync(cancellationToken);

        if (driver is null)
        {
            return;
        }
        _context.Remove(driver);
        _context.Remove(vehicle);
        await _context.SaveChangesAsync(cancellationToken);
    }
}