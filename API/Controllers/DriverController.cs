using Application.Commands;
using Application.Commands.Driver;
using Application.Dtos;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "fleet-admin")]
    [HttpGet]
    public async Task<IActionResult> Get(IQueryHandler<IEnumerable<DriverDto>> handler,CancellationToken cancellationToken)
    {
       var drivers =await handler.Handle(cancellationToken);
        
       
        return Ok(drivers);
    }
    [Authorize(Roles = "fleet-user")]
    [HttpPost]
    public async Task<IActionResult> Create(CreateDriverCommand command, CancellationToken cancellationToken)
    {
        await _createDriverCommandHandler.Handle(command, cancellationToken);
        return Created();
    }
    [Authorize(Roles = "fleet-user")]
    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> Update(UpdateDriverCommand command,Guid id, CancellationToken cancellationToken)
    {
        command.Id = id;
        await _updateDriverCommandHandler.Handle(command, cancellationToken);
        return NoContent();
    }
    [Authorize(Roles = "fleet-admin")]
    [HttpPost("assign")]
    public async Task<IActionResult> Assign(AssignDriverCommand command, CancellationToken cancellationToken)
    {
        await _assignDriverCommandHandler.Handle(command, cancellationToken);
        return Created();
    }
    [Authorize(Roles = "fleet-admin")]
    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> Delete(Guid id, ICommandHandler<DeleteDriverCommand> commandHandler,CancellationToken cancellationToken)
    {
        
        var command = new DeleteDriverCommand { Id = id };
        await commandHandler.Handle(command, cancellationToken);
        
        return NoContent();
    }
    
}