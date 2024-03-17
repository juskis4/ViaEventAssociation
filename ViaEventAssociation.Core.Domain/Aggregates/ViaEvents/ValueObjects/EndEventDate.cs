using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.Events;

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

        // Boundary for allowed end time (00:59 AM the next day)
        var allowedEndTime = date.Date.AddDays(1).AddSeconds(-1);  

        if (date.TimeOfDay > allowedEndTime.TimeOfDay) 
        {
            errors.Add("Event end time must fall within room usage hours (08:00 AM - 01:00 AM).");
        }
        return errors.Any() ? Result.Failure(errors.ToArray()) : Result.Success();
    }
}
