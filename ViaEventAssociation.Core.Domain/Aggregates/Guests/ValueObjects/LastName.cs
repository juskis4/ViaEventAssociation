using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.Guests;

public class LastName 
{
    public string Name { get; }

    private LastName(string name)
    {
        Name = name;
    }

    public static Result<LastName> Create(string name)
    {
        var validate = Validate(name);
        if (!validate.IsSuccess)
        {
            return Result<LastName>.Failure(validate.Errors.ToArray());
        }

        return Result<LastName>.Success(new LastName(name));
    }

    private static Result Validate(string name)
    {
        var errors = new List<string>();
        bool symbolCheck = name.Any(p => !char.IsLetterOrDigit(p));
        if (symbolCheck)
        {
            errors.Add("Last name cannot contains Special char.");
        }
        
        if (string.IsNullOrEmpty(name))
        {
            errors.Add("Last name cannot be NULL or Empty.");
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            errors.Add("Last name cannot bee a white space.");
        }

        if (name.Length < 2)
        {
            errors.Add("Last name cannot be less than 2 characters.");
        }

        if (name.Length > 16)
        {
            errors.Add("Last name cannot be more than 16 characters.");
        }

        if (errors.Any())
        {
            return Result.Failure(errors.ToArray());
        }
        return Result.Success();
    }
    
}