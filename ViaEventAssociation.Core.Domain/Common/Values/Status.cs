using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Common.Values;

public enum RequestStatus
{
    Pending,
    Accepted,
    Rejected
}
public class Status
{
    
    public RequestStatus RequestStatus { get; }

    private Status(RequestStatus requestStatus)
    {
        this.RequestStatus = requestStatus;
    }

    public static Result<Status> Create(RequestStatus requestStatus)
    {
        return Result<Status>.Success(new Status(requestStatus));
    }
}
