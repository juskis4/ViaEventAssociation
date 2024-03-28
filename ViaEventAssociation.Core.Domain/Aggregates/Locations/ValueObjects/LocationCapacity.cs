using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.Locations;

public class LocationCapacity
{
    public int Cap { get; }
    private LocationCapacity(int capacity)
    {
        Cap = capacity;
    }

    public static Result<LocationCapacity> Create(int capacity)
    {
        var val = Validate(capacity);
        if (!val.IsSuccess)
        {
            return Result<LocationCapacity>.Failure(val.Errors.ToArray());
        }
        return Result<LocationCapacity>.Success(new LocationCapacity(capacity));
    }
    private static Result Validate(int capacity)
    {
        var errors = new List<string>();

        if (capacity < 0)
        {
            errors.Add("Location Capacity cannot be less than 0");
        }

        if (capacity > 999)
        {
            errors.Add("Location Capacity cannot exceed 999");
        }
        return errors.Any() ? Result.Failure(errors.ToArray()) : Result.Success();
    }
}