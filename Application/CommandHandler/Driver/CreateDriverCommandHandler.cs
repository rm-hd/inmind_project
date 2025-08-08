using Application.Commands;
using Application.Commands.Driver;
using AutoMapper;
using Domain.Interfaces;
using Persistance;

namespace Application.CommandHandler.Driver;

public class CreateDriverCommandHandler: ICommandHandler<CreateDriverCommand>
{
    
    private readonly Context _context;
    private readonly IMapper _mapper;
    public CreateDriverCommandHandler(Context context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    

    public async Task Handle(CreateDriverCommand command, CancellationToken cancellationToken)
    {
        if (command is null)
        {
            return;
        }
        var driver = _mapper.Map<Domain.Entities.Driver>(command);
        await _context.Drivers.AddAsync(driver,cancellationToken);
        await _context.SaveChangesAsync(cancellationToken); 

        
    }
}