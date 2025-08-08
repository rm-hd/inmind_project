namespace Domain.Exceptions;

public class BadRequestException:DomainException
{
    public BadRequestException(string message) 
        : base(message, "BAD_REQUEST")
    {
        
    }
        
    public BadRequestException(string message, Exception innerException)
        : base(message, "BAD_REQUEST", innerException)
    {
    }
}