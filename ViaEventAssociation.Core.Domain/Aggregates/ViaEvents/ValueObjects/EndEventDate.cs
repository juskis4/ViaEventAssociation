using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.Events;

public class EndEventDate
{
    public DateTime EndDateTime { get; private set; }

    private EndEventDate(DateTime endDateTime)
    {
        EndDateTime = endDateTime;
    }

    public static Result<EndEventDate> Create(DateTime endDateTime)
    {
        var validationResult = Validate(endDateTime);
        if (!validationResult.IsSuccess)
        {
            return Result<EndEventDate>.Failure(validationResult.Errors.ToArray());
        }

        return Result<EndEventDate>.Success(new EndEventDate(endDateTime));
    }

    private static Result Validate(DateTime endDateTime)
    {
        var errors = new List<string>();

        if (endDateTime < DateTime.Now)  
        {
            errors.Add("Event end date cannot be in the past.");
        }

        if (endDateTime.TimeOfDay < new TimeSpan(8, 0, 0) || 
            endDateTime.TimeOfDay >= new TimeSpan(1, 0, 0)) 
        {
            errors.Add("Event end time must be between 08:00 AM and 01:00 AM (next day).");
        }

        // TODO: Discuss with teacher
        // End date must be after the start date 

        // TODO: Discuss with teacher
        // Event duration cannot exceed 10 hours

        return errors.Any() ? Result.Failure(errors.ToArray()) : Result.Success();
    }
}
