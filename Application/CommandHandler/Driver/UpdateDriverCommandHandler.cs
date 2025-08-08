using Application.Commands;
using Application.Commands.Driver;
using AutoMapper;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistance;

namespace Application.CommandHandler.Driver;

public class UpdateDriverCommandHandler: ICommandHandler<UpdateDriverCommand>
{
    
    private readonly Context _context;
    private readonly IMapper _mapper;
    public UpdateDriverCommandHandler(Context context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task Handle(UpdateDriverCommand command, CancellationToken cancellationToken)
    {

        if (command is  null)
        {
            return;
        }

       
        var driver = await _context.Drivers.FirstOrDefaultAsync(d=> d.Id.Equals(command.Id), cancellationToken);
        if (driver is null)
        {
            return;
        }
        driver.LicenseNumber = command.LicenseNumber;
        driver.Name = command.Name;
        driver.VehicleId = command.VehicleId;
        
        await _context.SaveChangesAsync(cancellationToken);
    }
    
    
    
}