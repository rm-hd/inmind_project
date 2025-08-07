using Application.Commands;
using common.Dtos;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class DriverController: ControllerBase
{
    
    private readonly ICommandHandler<CreateDriverCommand> _createDriverCommandHandler;
    private readonly ICommandHandler<UpdateDriverCommand> _updateDriverCommandHandler;
    private readonly ICommandHandler<AssignDriverCommand> _assignDriverCommandHandler;
    

    public DriverController(ICommandHandler<CreateDriverCommand> createDriverCommandHandler,
        ICommandHandler<UpdateDriverCommand> updateDriverCommandHandler,
        ICommandHandler<AssignDriverCommand> assignDriverCommandHandler)
    {
        _createDriverCommandHandler = createDriverCommandHandler;
        _updateDriverCommandHandler = updateDriverCommandHandler;
        _assignDriverCommandHandler = assignDriverCommandHandler;
    }

    [HttpGet]
    public async Task<IActionResult> Get(IQueryHandler<IEnumerable<DriverDto>> handler,CancellationToken cancellationToken)
    {
        var drivers = handler.Handle(cancellationToken);
        return Ok(drivers);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(CreateDriverCommand command, CancellationToken cancellationToken)
    {
        await _createDriverCommandHandler.Handle(command, cancellationToken);
        return Created();
    }

    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> Update(UpdateDriverCommand command,Guid id, CancellationToken cancellationToken)
    {
        command.Id = id;
        await _updateDriverCommandHandler.Handle(command, cancellationToken);
        return NoContent();
    }

    [HttpPost("assign")]
    public async Task<IActionResult> Assign(AssignDriverCommand command, CancellationToken cancellationToken)
    {
        await _assignDriverCommandHandler.Handle(command, cancellationToken);
        return Created();
    }
    
    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> Delete(Guid id, ICommandHandler<DeleteDriverCommand> commandHandler,CancellationToken cancellationToken)
    {
        
        var command = new DeleteDriverCommand { Id = id };
        await commandHandler.Handle(command, cancellationToken);
        
        return NoContent();
    }
    
}