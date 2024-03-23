
using ViaEventAssociation.Core.Domain.Aggregates.Events;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.Guests.ValueObjects;

public class GuestId
{
    public Guid Id { get; private set; }

    private GuestId(Guid id)
    {
        this.Id = id;
    }

    public static Result<GuestId> Create()
    {
        var guestId = new GuestId(Guid.NewGuid());
        return Result<GuestId>.Success(guestId);
    }

    public static Result<GuestId> Create(string guestIdSting)
    {
        var validationResult = Validate(guestIdSting);
        if (!validationResult.IsSuccess)
        {
            return Result<GuestId>.Failure(validationResult.Errors.ToArray());
        }

        return Result<GuestId>.Success(new GuestId(Guid.Parse(guestIdSting)));
    }

    private static Result Validate(string guestIdString)
    {
        var errors = new List<string>();

        if (!Guid.TryParse(guestIdString, out _))
        {
            errors.Add("Invalid GuestId format.");
        }

        return errors.Any() ? Result.Failure(errors.ToArray()) : Result.Success();
    }
}