namespace Domain.Exceptions;

public class NotFoundException:DomainException
{
    public NotFoundException(string entityName, object id)
        : base($"{entityName} with ID '{id}' was not found.", "ENTITY_NOT_FOUND")
    {
    }

    public NotFoundException(string message)
        : base(message, "NOT_FOUND")
    {
    }
}