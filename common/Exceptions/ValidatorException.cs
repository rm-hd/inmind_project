namespace Domain.Exceptions;

public class ValidatorException:DomainException
{
    protected ValidatorException(string message) : base(message,"Validation Exception")
    {
    }
    
    protected ValidatorException(string message, Exception innerException) : base(message,"Validation Exception", innerException)
    {
    }
}