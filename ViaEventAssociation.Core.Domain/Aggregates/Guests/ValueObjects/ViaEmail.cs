using System.Text.RegularExpressions;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.Guests;

public class ViaEmail
{
    public string Email { get; }

    private ViaEmail(string email)
    {
        this.Email = email.ToLower();
    }

    public static Result<ViaEmail> Create(string email)
    {
        var validate = Validate(email);
        return !validate.IsSuccess ? Result<ViaEmail>.Failure(validate.Errors.ToArray()) : Result<ViaEmail>.Success(new ViaEmail(email));
    }

    private static Result Validate(string email)
    {
        List<string> errors = new List<string>();

        // Check if email ends with "@via.dk"
        if (!email.EndsWith("@via.dk", StringComparison.OrdinalIgnoreCase))
        {
            errors.Add("Email must end with '@via.dk'.");
        }

        if (string.IsNullOrEmpty(email))
        {
            errors.Add("Email cannot bee null er empty.");
        }
        if (string.IsNullOrWhiteSpace(email))
        {
            errors.Add("Email cannot bee a white space.");
        }
        
        // Check if email is in correct format
        string emailRegexPattern = @"^[a-zA-Z0-9]{3,6}@[a-zA-Z0-9]+\.[a-zA-Z]{2,4}$";
        if (!Regex.IsMatch(email, emailRegexPattern))
        {
            errors.Add("Email format is incorrect Consider using English Letters.");
        }

        // Check if email text1 is between 3 and 6 characters long
        string[] parts = email.Split('@');
        if (parts.Length != 2 || parts[0].Length < 3 || parts[0].Length > 6)
        {
            errors.Add("Email name must be between 3 and 6 characters long.");
        }
        else
        {
            // Check if email text1 matches the specified criteria
            string text1 = parts[0];
            if (!Regex.IsMatch(text1, @"^[a-zA-Z]{3,4}$") && !Regex.IsMatch(text1, @"^\d{6}$"))
            {
                errors.Add("Email name must be either 3 or 4  English letters, or 6 digits from 0 to 9.");
            }
        }
        
        return errors.Any() ? Result.Failure(errors.ToArray()) : Result.Success();
    }
}