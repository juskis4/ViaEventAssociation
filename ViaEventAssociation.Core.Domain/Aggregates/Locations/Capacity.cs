using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.Locations;

public class Capacity
{
    private int Cap { get; }
    public Capacity(int capacity)
    {
        Cap = capacity;
    }

    private static Result<Capacity> Create(int capacity)
    {
        var val = Validate(capacity);
        if (!val.IsSuccess)
        {
            return Result<Capacity>.Failure(val.Errors.ToArray());
        }
        return Result<Capacity>.Success(new Capacity(capacity));
    }
    private static Result Validate(int capacity)
    {
        var errors = new List<string>();

        if (capacity < 0)
        {
            errors.Add("Capacity cannot be less than 0");
        }
        if (errors.Any())
        {
            return Result.Failure(errors.ToArray());
        }
        return Result.Success();
    }
}