using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.Events;

public class Description
{
    public string DescriptionText { get; private set; }

    private Description(string descriptionText)
    {
        DescriptionText = descriptionText;
    }

    public static Result<Description> Create(string descriptionText)
    {
        var validationResult = Validate(descriptionText);
        if (!validationResult.IsSuccess)
        {
            return Result<Description>.Failure(validationResult.Errors.ToArray());
        }

        return Result<Description>.Success(new Description(descriptionText));
    }

    private static Result Validate(string descriptionText)
    {
        var errors = new List<string>();

        if (string.IsNullOrEmpty(descriptionText))
        {
            descriptionText = "";
        }

        if (descriptionText.Length > 250)
        {
            errors.Add("Description cannot exceed 250 characters.");
        }

        return errors.Any() ? Result.Failure(errors.ToArray()) : Result.Success();
    }
}
