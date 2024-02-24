namespace ViaEventAssociation.Core.Tools.OperationResult;

public class Result<T>
{
    public T Data { get; private set; } = default!;
    public string ErrorMessage { get; private set; } = null!;
    public Exception Exception { get; private set; } = null!;
    
    public bool IsSuccess => string.IsNullOrEmpty(ErrorMessage) && Exception == null;


    public Result(T data) => Data = data;

    public Result(string error) => ErrorMessage = error;
    
    public Result(Exception ex) => Exception = ex;

}

