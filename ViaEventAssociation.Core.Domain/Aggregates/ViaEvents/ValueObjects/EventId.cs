using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.Events;

public class EventId
{
    public Guid Id { get; private set; } 

    private EventId(Guid id)
    {
        Id = id;
    }

    public static Result<EventId> Create()
    {
        var eventId = new EventId(Guid.NewGuid());
        return Result<EventId>.Success(eventId);
    }

    public static Result<EventId> Create(string eventIdString)
    {
        var validationResult = Validate(eventIdString);
        if (!validationResult.IsSuccess)
        {
            return Result<EventId>.Failure(validationResult.Errors.ToArray());
        }

        return Result<EventId>.Success(new EventId(Guid.Parse(eventIdString)));
    }

    private static Result Validate(string eventIdString)
    {
        var errors = new List<string>();

        if (!Guid.TryParse(eventIdString, out _))
        {
             errors.Add("Invalid EventId format.");
        }

        return errors.Any() ? Result.Failure(errors.ToArray()) : Result.Success();
    }
}
