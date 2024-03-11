using System.Dynamic;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.Locations;

public class LocationId
{
    private int Id { get; }

    private LocationId(int id)
    {
        Id = id;
    }

    public static Result<LocationId> Create(int id)
    {
        var val = Validate(id);
        if (!val.IsSuccess)
        {
            return Result<LocationId>.Failure(val.Errors.ToArray());
        }

        return Result<LocationId>.Success(new LocationId(id));
    }

    private static Result Validate(int id)
    {
        return Result.Success();
    }
}