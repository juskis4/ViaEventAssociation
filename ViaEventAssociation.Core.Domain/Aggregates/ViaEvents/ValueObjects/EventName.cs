using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.Events.ValueObjects;

public class EventName
{
    public string Title { get; private set;}

    private EventName(string title)
    {
        Title = title;
    }

    public static Result<EventName> Create(string title)
    {
        var validate = Validate(title);
        if (!validate.IsSuccess)
        {
            return Result<EventName>.Failure(validate.Errors.ToArray());
        }

        return Result<EventName>.Success(new EventName(title));
    }

    private static Result Validate(string title)
    {
        var errors = new List<string>();

        if (string.IsNullOrEmpty(title))
        {
            errors.Add("Title cannot be NULL or Empty.");
        } else 
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                errors.Add("Title cannot bee a white space.");
            }

            if (title.Length < 3)
            {
                errors.Add("Title cannot be less than 3 characters.");
            }

            if (title.Length > 75)
            {
                errors.Add("Title cannot be more than 75 characters.");
            }
        } 

        return errors.Any() ? Result.Failure(errors.ToArray()) : Result.Success();
    }

}
