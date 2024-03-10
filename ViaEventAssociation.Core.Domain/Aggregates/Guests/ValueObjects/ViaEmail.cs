using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.Guests;

public class ViaEmail
{
    public string Email { get; }

    private ViaEmail(string email)
    {
        this.Email = email;
    }

    public static Result<ViaEmail> Create(string email)
    {
        var validate = Validate(email);
        return !validate.IsSuccess ? Result<ViaEmail>.Failure(validate.Errors.ToArray()) : Result<ViaEmail>.Success(new ViaEmail(email));
    }

    private static Result Validate(string email)
    {
        List<string> errors = new List<string>();
        if (string.IsNullOrEmpty(email))
        {
            errors.Add("Email cannot bee Empty.");
        }

        if (string.IsNullOrWhiteSpace(email))
        {
            errors.Add("Email cannot bee a white space.");
        }

        if (email.Count(c => c == '@') != 1)
        {
         
            errors.Add("Email should contain one @.");
        }
        string[] validDomains = { "via", "viauc" };
        string[] parts = email.Split("@");
        var domain = parts.Length == 2 ? parts[1].Split(".")[0] : ""; // Get the domain part after "@"
        if (!validDomains.Contains(domain.ToLower()))
        {
            errors.Add("Email domain must be 'via' or 'viauc'.");
        }
        
        if (!email.EndsWith(".dk", StringComparison.OrdinalIgnoreCase))
        {
            errors.Add("Email must end with '.dk'.");
        }

        return errors.Any() ? Result.Failure(errors.ToArray()) : Result.Success();
    }
}