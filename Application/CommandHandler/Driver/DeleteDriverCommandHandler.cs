using Application.Commands;
using AutoMapper;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistance;

namespace Application.CommandHandler.Driver;

public class DeleteDriverCommandHandler: ICommandHandler<DeleteDriverCommand>
{
    private readonly Context _context;
    private readonly IMapper _mapper;
    public DeleteDriverCommandHandler(Context context, IMapper mapper)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task Handle(DeleteDriverCommand command, CancellationToken cancellationToken)
    {
        if (command is  null)
        {
            return;
        }
        var driver = await _context.Drivers.FirstOrDefaultAsync(d=>d.Id.Equals(command.Id),cancellationToken);

        if (driver is null)
        {
            return;
        }

        _context.Remove(driver);
        await _context.SaveChangesAsync(cancellationToken);



    }
    
}