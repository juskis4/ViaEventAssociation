using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.Events;

public enum Status
{
    Draft,
    Ready,
    Active,
    Cancelled
}

public class ViaEvent
{
    public EventName Title { get; set; }
    public Capacity Capacity { get; set; }
    public Description Description { get; set; }
    public EndEventDate EndDate { get; set; }
    public StartEventDate StartDate { get; set; }
    public bool IsPublic { get; set; } = false;
    public EventId Id { get; set; }
    public Status Status { get; set; }

    private ViaEvent(EventId id, EventName title, Description description, StartEventDate startDate,
        EndEventDate endDate, Capacity capacity, bool isPublic, Status status)
    {
        Capacity = capacity;
        Description = description;
        EndDate = endDate;
        StartDate = startDate;
        IsPublic = isPublic;
        Title = title;
        Status = status;

    }

    private ViaEvent(EventId id, EventName title, Description description, Capacity capacity, bool isPublic = false)
    {
        Capacity = capacity;
        Description = description;
        IsPublic = isPublic;
        Title = title;
        Status = Status.Draft;
    }



    public static Result<ViaEvent> Create(EventId id)
    {
        var errors = new List<string>();
        var capacity = Capacity.Create(5);
        var title = EventName.Create("Working Title");
        var description = Description.Create("");
        if (!capacity.IsSuccess)
        {
            errors.AddRange(capacity.Errors);
        }
        if (!title.IsSuccess)
        {
            errors.AddRange(title.Errors);
        }
        if (!description.IsSuccess)
        {
            errors.AddRange(description.Errors);
        }

        return !errors.Any() ? Result<ViaEvent>.Success(new ViaEvent(id, title.Data, description.Data, capacity.Data)) : Result<ViaEvent>.Failure(errors.ToArray());
    }

    public static Result<ViaEvent> Create(EventId id, EventName title, Description description,
        StartEventDate startDate, EndEventDate endDate, Capacity capacity, bool isPublic, Status status)
    {
        return Result<ViaEvent>.Success(
            new ViaEvent(id, title, description, startDate, endDate, capacity, isPublic, status));
    }

    public static Result UpdateEventTitle(ViaEvent viaEvent, string newTitle)
    {

        var stats = viaEvent.Status;
        if (stats == Status.Draft || stats == Status.Ready)
        {
            var title = EventName.Create(newTitle);
            if (!title.IsSuccess)
            {
                return Result.Failure(title.Errors.ToArray());
            }

            viaEvent.Title = title.Data;
            return Result.Success();
        }

        return Result.Failure($"an {stats} event cannot be modified");
    }

    public Result UpdateEventTimes(StartEventDate newStartDate, EndEventDate newEndDate)
    {
        var errors = new List<string>();

        if (Status != Status.Draft && Status != Status.Ready)
        {
            errors.Add("Event cannot be modified in its current status");
        }

        if (newStartDate.Date > newEndDate.Date)
        {
            errors.Add("Start date cannot be after end date");
        }

        // Check if the start date is the same as the end date but the start time is after the end time
        if (newStartDate.Date.Date == newEndDate.Date.Date && newStartDate.Date.TimeOfDay >= newEndDate.Date.TimeOfDay)
        {
            errors.Add("Start time must be before end time on the same day");
        }

        // Duration check
        var duration = newEndDate.Date.Subtract(newStartDate.Date);
        if (duration.TotalHours < 1 || duration.TotalHours > 10)
        {
            errors.Add("Event duration must be between 1 and 10 hours");
        }

        if (!errors.Any())
        {
            StartDate = newStartDate;
            EndDate = newEndDate;

            if (Status == Status.Ready)
            {
                Status = Status.Draft;
            }

            return Result.Success();
        }

        return Result.Failure(errors.ToArray());
    }
}
