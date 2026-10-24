namespace Application.ApplicationResult;

public class Result<T> 
{
    public T? Value { get; private set; }
    public ResultStatus Status { get; set; }
    
    public static Result<T> Success(T value)
    {
        return new Result<T> { Status = ResultStatus.Succeeded, Value = value };
    }
    
    public  static Result<T> Failure(ResultStatus status)
    {
        return new Result<T> { Status = status ,Value =default};
    }

}

public class Result
{
    public ResultStatus Status { get; set; }
    
    public static Result Success()
    {
        return new Result { Status = ResultStatus.Succeeded};
    }

    public static Result Failure(ResultStatus status)
    {
        return new Result { Status = status};
    }

    public override bool Equals(object? obj)
    {
        return this.Status == ((Result)obj).Status;
    }
}