using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.Requests.ValueObjects;

public class RequestId
{
    public Guid Id { get; set; }

    private RequestId(Guid id)
    {
        this.Id = id;
    }

    public static Result<RequestId> Create()
    {
        var requestId = new RequestId(Guid.NewGuid());
        return Result<RequestId>.Success(requestId);
    }

    public static Result<RequestId> Create(string id)
    {
        var validate = Validate(id);
        if (!validate.IsSuccess)
        {
            return Result<RequestId>.Failure(validate.Errors.ToArray());
        }

        return Result<RequestId>.Success(new RequestId(Guid.Parse(id)));
    }

    private static Result Validate(string id)
    {
        var errors = new List<string>();

        if (!Guid.TryParse(id, out _))
        {
            errors.Add("Invalid RequestId format.");
        }

        return errors.Any() ? Result.Failure(errors.ToArray()) : Result.Success();
    }
}