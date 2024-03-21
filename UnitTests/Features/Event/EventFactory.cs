using ViaEventAssociation.Core.Domain.Aggregates.Events;
using ViaEventAssociation.Core.Domain.Aggregates.Events.ValueObjects;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace UnitTests.Features.Event;

// Factory class to easily construct an event in a specific state
public class EventFactory
{
    public static Result<ViaEvent> CreateEventInDraftStatus()
    {
        var eventId = EventId.Create();
        var title = EventName.Create("Test Event in Draft state");
        var description = Description.Create("Test description");
        var startDate = StartEventDate.Create(new DateTime(2026, 12, 25, 9, 0, 0));
        var endDate = EndEventDate.Create(new DateTime(2026, 12, 25, 12, 0, 0));
        var capacity = Capacity.Create(10);

        var results = new Result[] { eventId, title, description, startDate, endDate, capacity };
        if (results.All(r => r.IsSuccess))
        {
            return ViaEvent.Create(eventId.Data, title.Data, description.Data, startDate.Data, endDate.Data, capacity.Data, default, Status.Draft);
        }
        else
        {
            var errors = results.Where(r => !r.IsSuccess)
                                .SelectMany(r => r.Errors)
                                .ToArray();
            return Result<ViaEvent>.Failure(errors);
        }
    }

    public static Result<ViaEvent> CreateEventInReadyStatus()
    {
        var eventId = EventId.Create();
        var title = EventName.Create("Test Event in Ready state");
        var description = Description.Create("Test description");
        var startDate = StartEventDate.Create(new DateTime(2026, 12, 25, 9, 0, 0));
        var endDate = EndEventDate.Create(new DateTime(2026, 12, 25, 12, 0, 0));
        var capacity = Capacity.Create(10);

        var results = new Result[] { eventId, title, description, startDate, endDate, capacity };
        if (results.All(r => r.IsSuccess))
        {
            return ViaEvent.Create(eventId.Data, title.Data, description.Data, startDate.Data, endDate.Data, capacity.Data, default, Status.Ready);
        }
        else
        {
            var errors = results.Where(r => !r.IsSuccess)
                                .SelectMany(r => r.Errors)
                                .ToArray();
            return Result<ViaEvent>.Failure(errors);
        }
    }

    public static Result<ViaEvent> CreateEventInCancelledStatus()
    {
        var eventId = EventId.Create();
        var title = EventName.Create("Test Event in Cancelled state");
        var description = Description.Create("Test description");
        var startDate = StartEventDate.Create(new DateTime(2026, 12, 25, 9, 0, 0));
        var endDate = EndEventDate.Create(new DateTime(2026, 12, 25, 12, 0, 0));
        var capacity = Capacity.Create(10);

        var results = new Result[] { eventId, title, description, startDate, endDate, capacity };
        if (results.All(r => r.IsSuccess))
        {
            return ViaEvent.Create(eventId.Data, title.Data, description.Data, startDate.Data, endDate.Data, capacity.Data, default, Status.Cancelled);
        }
        else
        {
            var errors = results.Where(r => !r.IsSuccess)
                                .SelectMany(r => r.Errors)
                                .ToArray();
            return Result<ViaEvent>.Failure(errors);
        }
    }

    public static Result<ViaEvent> CreateEventInActiveStatus()
    {
        var eventId = EventId.Create();
        var title = EventName.Create("Test Event in Active state");
        var description = Description.Create("Test description");
        var startDate = StartEventDate.Create(new DateTime(2026, 12, 25, 9, 0, 0));
        var endDate = EndEventDate.Create(new DateTime(2026, 12, 25, 12, 0, 0));
        var capacity = Capacity.Create(10);

        var results = new Result[] { eventId, title, description, startDate, endDate, capacity };
        if (results.All(r => r.IsSuccess))
        {
            return ViaEvent.Create(eventId.Data, title.Data, description.Data, startDate.Data, endDate.Data, capacity.Data, default, Status.Active);
        }
        else
        {
            var errors = results.Where(r => !r.IsSuccess)
                                .SelectMany(r => r.Errors)
                                .ToArray();
            return Result<ViaEvent>.Failure(errors);
        }
    }
}
