namespace Domain.Exceptions;

public class BadUsageException : Exception
{
    public BadUsageException()
    { }

    public BadUsageException(string message)
        : base(message) { }

    public BadUsageException(string message, Exception innerException)
        : base(message, innerException) { }
    
    public override string ToString()
    {
        return $"BadUsageException: {base.ToString()}";
    }
}