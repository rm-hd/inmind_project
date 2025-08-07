namespace Domain.Interfaces;

public interface IQueryHandler<T> where T : class
{
    public Task<T> Handle(CancellationToken cancellationToken);
}