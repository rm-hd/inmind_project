using Application.Commands;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

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

    [HttpPost]
    public async Task<IActionResult> Create(CreateDriverCommand command, CancellationToken cancellationToken)
    {
        await _createDriverCommandHandler.Handle(command, cancellationToken);
        return Ok();
    }

    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> Update(UpdateDriverCommand command, CancellationToken cancellationToken)
    {
        await _updateDriverCommandHandler.Handle(command, cancellationToken);
        return Ok();
    }

    [HttpPost("assign")]
    public async Task<IActionResult> Assign(AssignDriverCommand command, CancellationToken cancellationToken)
    {
        await _assignDriverCommandHandler.Handle(command, cancellationToken);
        return Ok();
    }
    
}