namespace ViaEventAssociation.Core.Tools.OperationResult;

public class Result
{
    private readonly List<string> _errors = new List<string>();
    public IEnumerable<string> Errors => _errors.AsReadOnly();
    public bool IsSuccess => !Errors.Any();

    protected Result() { }

    // Factory method
    public static Result Success() => new Result();

    // Factory method
    // Example: var failureResult = Result.Failure("Error 1", "Error 2", "Error 3");
    public static Result Failure(params string[] errors)
    {
        var result = new Result();
        result._errors.AddRange(errors);
        return result;
    }

    // This will be useful when the result will have errors from different
    // places of our system, making it possible to add errors to already created result.
    // Also used by generic implementation of Result
    public void AddError(string error) => _errors.Add(error);
    public Array GetErrors() => _errors.ToArray();
}

public class Result<T> : Result
{
    public T Data { get; private set; }

    private Result(T data)
    {
        Data = data;
    }

    // Factory method
    public static Result<T> Success(T data) => new Result<T>(data);

    // Factory method
    // Example: var failureResult = Result<T>.Failure("Error 1", "Error 2", "Error 3");
    public new static Result<T> Failure(params string[] errors)
    {
        var result = new Result<T>(default!);
        foreach (var error in errors)
        {
            result.AddError(error);
        }
        return result;
    }
}