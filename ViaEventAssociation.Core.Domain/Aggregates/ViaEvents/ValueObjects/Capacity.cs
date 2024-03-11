using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.Events;

public class Capacity
{
    public int CapacityCount { get; private set; }

    private Capacity(int capacityCount)
    {
        CapacityCount = capacityCount;
    }

    public static Result<Capacity> Create(int capacityCount)
    {
        var validationResult = Validate(capacityCount);
        if (!validationResult.IsSuccess)
        {
            return Result<Capacity>.Failure(validationResult.Errors.ToArray());
        }

        return Result<Capacity>.Success(new Capacity(capacityCount));
    }

    private static Result Validate(int capacityCount)
    {
        var errors = new List<string>();

        if (capacityCount < 5)
        {
            errors.Add("Capacity cannot be less than 5");
        }

        if (capacityCount > 50)
        {
            errors.Add("Capacity cannot be greater than 50");
        }

        return errors.Any() ? Result.Failure(errors.ToArray()) : Result.Success();
    }
}
