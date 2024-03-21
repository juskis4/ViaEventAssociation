using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.Events.ValueObjects;

public class StartEventDate
{
    public DateTime Date { get; private set; } 

    private StartEventDate(DateTime date)
    {
        Date = date;
    }

    public static Result<StartEventDate> Create(DateTime date)
    {
        var validationResult = Validate(date);
        if (!validationResult.IsSuccess)
        {
            return Result<StartEventDate>.Failure(validationResult.Errors.ToArray());
        }

        return Result<StartEventDate>.Success(new StartEventDate(date));
    }

    private static Result Validate(DateTime date)
    {
        var errors = new List<string>();

        if (date < DateTime.Now)  
        {
            errors.Add("Event start date cannot be in the past.");
        }

        if (date.TimeOfDay < new TimeSpan(8, 0, 0) || 
            date.TimeOfDay >= new TimeSpan(23, 59, 59)) 
        {
            errors.Add("Event start time must be between 08:00 AM and 11:59 PM.");
        }

        return errors.Any() ? Result.Failure(errors.ToArray()) : Result.Success();
    }
}
