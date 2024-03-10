using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.Guests;

public class FirstName
{
    public string Name { get; }

    private FirstName(string name)
    {
        Name = name;
    }

    public static Result<FirstName> Create(string name)
    {
        var validate = Validate(name);
        if (!validate.IsSuccess)
        {
            return Result<FirstName>.Failure(validate.Errors.ToArray());
        }

        return Result<FirstName>.Success(new FirstName(name));
    }

    private static Result Validate(string name)
    {
        var errors = new List<string>();
        bool symbolCheck = name.Any(p => !char.IsLetterOrDigit(p));
        if (symbolCheck)
        {
            errors.Add("First name cannot contains Special char.");
        }
        
        if (string.IsNullOrEmpty(name))
        {
            errors.Add("First name cannot be NULL or Empty.");
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            errors.Add("First name cannot bee a white space.");
        }

        if (name.Length < 3)
        {
            errors.Add("First name cannot be less than 3 characters.");
        }

        if (name.Length > 16)
        {
            errors.Add("First name cannot be more than 16 characters.");
        }

        if (errors.Any())
        {
            return Result.Failure(errors.ToArray());
        }
        return Result.Success();
    }
}