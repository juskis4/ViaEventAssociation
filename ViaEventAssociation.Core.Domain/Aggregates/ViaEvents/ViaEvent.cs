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
    
    private  ViaEvent(EventId id, EventName title, Description description,StartEventDate startDate, 
        EndEventDate endDate, Capacity capacity, bool isPublic, Status status)
    {
        Id = id;
        Capacity = capacity;
        Description = description; 
        EndDate = endDate;
        StartDate = startDate;
        IsPublic = isPublic;
        Title = title;
        Status = status;

    }
    
    private  ViaEvent(EventId id,EventName title, Description description,Capacity capacity , bool isPublic = false)
    {
        Id = id;
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
        StartEventDate startDate,EndEventDate endDate, Capacity capacity, bool isPublic, Status status)
    {
        return Result<ViaEvent>.Success(
            new ViaEvent(id, title, description, startDate, endDate, capacity, isPublic, status));
    }

    public static Result UpdateEventTitle(ViaEvent viaEvent,string newTitle)
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

    // UC3 - Update Description
    public Result UpdateDescription(string newDescription)
    {
        if (Status != Status.Draft && Status != Status.Ready) 
        {
            return Result.Failure($"Event in status {Status} cannot be modified.");
        }

        var descriptionResult = Description.Create(newDescription);
        if (!descriptionResult.IsSuccess)
        {
            return Result.Failure(descriptionResult.Errors.ToArray());
        }

        Description = descriptionResult.Data;

        // UC3 - S3
        if (Status == Status.Ready)
        {
            Status = Status.Draft;  
        } 
    }


    public Result MakeEventPublic()
    {
        if (Status == Status.Cancelled)
        {
            return Result.Failure("a cancelled event cannot be modified");
        }
        IsPublic = true;

        return Result.Success();
    }

    public Result MakeEventPrivate()
    {
        if (Status == Status.Cancelled || Status == Status.Active)
        {
            return Result.Failure($"a {Status} event cannot be modified");
        }
        IsPublic = false;
        Status = Status.Draft;

        return Result.Success();
    }

}
