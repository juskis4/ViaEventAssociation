using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.Locations;

public class Address
{
    private string Name { get; }

    private Address(string name)
    {
        Name = name;
    }

    public static Result<Address> Create(string name)
    {
        var val = Validate(name);
        if (!val.IsSuccess)
        {
            return Result<Address>.Failure(val.Errors.ToArray());
        }

        return Result<Address>.Success(new Address(name));
    }
    private static Result Validate(string name)
    {
        var errors = new List<string>();

        if (string.IsNullOrEmpty(name))
        {
            errors.Add("Address cannot be NULL or empty");
        }

        if (errors.Any())
        {
            return Result.Failure(errors.ToArray());
        }
        return Result.Success();
    }
}