using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.Locations;

public class LocationName
{
    public string Name { get; private set; }

    private LocationName(string name)
    {
        Name = char.ToUpper(name[0]) + name.Substring(1).ToLower();
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

        if (name.Length < 2)
        {
            errors.Add("Location name cannot be less than 2 characters");
        }

        if (name.Length > 25)
        {
            errors.Add("Location name cannot exceed 25 characters");
        }
        if (errors.Any())
        {
            return Result.Failure(errors.ToArray());
        }
        return Result.Success();
    }
}