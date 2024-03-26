using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.Locations;

public class Address
{
    private string Name { get; }

    private Address(string name)
    {
        Name = Name = char.ToUpper(name[0]) + name.Substring(1).ToLower();;
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
        
        if (name.Length < 2)
        {
            errors.Add("Location address cannot be less than 2 characters");
        }

        if (name.Length > 25)
        {
            errors.Add("Location address cannot be exceed 25 characters");
        }
        if (errors.Any())
        {
            return Result.Failure(errors.ToArray());
        }
        return Result.Success();
    }
}