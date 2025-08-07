namespace Domain.Interfaces;

public interface ICommandHandler<T> where T : class
{
    public Task Handle(T command, CancellationToken cancellationToken);
}