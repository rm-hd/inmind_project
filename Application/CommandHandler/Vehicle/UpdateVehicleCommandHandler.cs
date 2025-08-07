using Application.Commands.Vehicle;
using AutoMapper;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistance;

namespace Application.CommandHandler.Vehicle;

public class UpdateVehicleCommandHandler: ICommandHandler<UpdateVehicleCommand>
{
    private readonly Context _context;
    private readonly IMapper _mapper;

    public UpdateVehicleCommandHandler(Context context, IMapper mapper)
    {
        _mapper = mapper;
        _context = context;

    }

    public async Task Handle(UpdateVehicleCommand command, CancellationToken cancellationToken)
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

        vehicle.PlateNumber = command.PlateNumber;
        vehicle.Model = command.Model;
        vehicle.Status = command.Status;
        vehicle.DriverId = command.DriverId;


        await _context.SaveChangesAsync(cancellationToken);
    }
}



