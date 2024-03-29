using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.Events.ValueObjects;

public class EndEventDate
{
    public DateTime Date { get; private set; }

    private EndEventDate(DateTime date)
    {
        Date = date;
    }

    public static Result<EndEventDate> Create(DateTime date)
    {
        var validationResult = Validate(date);
        if (!validationResult.IsSuccess)
        {
            return Result<EndEventDate>.Failure(validationResult.Errors.ToArray());
        }

        return Result<EndEventDate>.Success(new EndEventDate(date));
    }

    private static Result Validate(DateTime date)
    {
        var errors = new List<string>();

        // Allowed start time (8:00 AM on the same day)
        var allowedStartTime = date.Date.AddHours(8);

        // Allowed end time (1:00 AM the next day)
        var allowedEndTime = date.Date.AddDays(1).AddHours(1);
        
        // Check if end time is outside the allowed range 
        if (date.TimeOfDay < allowedStartTime.TimeOfDay && date.TimeOfDay > allowedEndTime.TimeOfDay)
        {
            errors.Add("Event end time must fall within room usage hours (08:00 AM - 01:00 AM).");
        }

        return errors.Any() ? Result.Failure(errors.ToArray()) : Result.Success();
    }
    
}
