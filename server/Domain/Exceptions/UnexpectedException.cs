namespace Domain.Exceptions;

public class UnexpectedException : Exception
{
    public UnexpectedException()
    { }
    
    public UnexpectedException(string message)
        : base(message) { }
    
    public UnexpectedException(string message, Exception innerException)
        : base(message, innerException)
    { }
    
    public override string ToString()
    {
        return $"UnexpectedException: {base.ToString()}";
    }
}