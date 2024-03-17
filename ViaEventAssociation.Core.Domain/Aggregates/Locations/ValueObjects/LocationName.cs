using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.Locations;

public class LocationName
{
    private string Name { get; }
    private LocationName(string name)
    {
        Name = name;
    }

    public static Result<LocationName> Create(string name)
    {
        var val = Validate(name);
        if (!val.IsSuccess)
        {
            return Result<LocationName>.Failure(val.Errors.ToArray());
        }

        return Result<LocationName>.Success(new LocationName(name));
    }
    private static Result Validate(string name)
    {
        var errors = new List<string>();

        if (string.IsNullOrEmpty(name)||string.IsNullOrWhiteSpace(name))
        {
            errors.Add("Location Name cannot be NULL or empty");
        }

        if (errors.Any())
        {
            return Result.Failure(errors.ToArray());
        }
        return Result.Success();
    }
}