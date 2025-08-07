using Application.Commands.Vehicle;
using AutoMapper;
using Domain.Interfaces;
using Persistance;

namespace Application.CommandHandler.Vehicle;

public class CreateVehicleCommandHandler:ICommandHandler<CreateVehicleCommand>
{
    private readonly Context _context;
    private readonly IMapper _mapper;
    public CreateVehicleCommandHandler(Context context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    

    public async Task Handle(CreateVehicleCommand command, CancellationToken cancellationToken)
    {
        if (command is  null)
        {
            return;
        }
        var vehicle = _mapper.Map<Domain.Entities.Vehicle>(command);
        await _context.Vehicles.AddAsync(vehicle,cancellationToken);
        await _context.SaveChangesAsync(cancellationToken); 

        
    }
}