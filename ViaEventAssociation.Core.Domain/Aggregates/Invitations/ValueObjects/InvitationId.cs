using ViaEventAssociation.Core.Domain.Aggregates.Requests.ValueObjects;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.Invitations.ValueObjects;

public class InvitationId
{
    public Guid Id { get; set; }

    private InvitationId(Guid id)
    {
        this.Id = id;
    }

    public static Result<InvitationId> Create()
    {
        var invitationId = new InvitationId(Guid.NewGuid());
        return Result<InvitationId>.Success(invitationId);
    }

    public static Result<InvitationId> Create(string id)
    {
        var validate = Validate(id);
        if (!validate.IsSuccess)
        {
            return Result<InvitationId>.Failure(validate.Errors.ToArray());
        }

        return Result<InvitationId>.Success(new InvitationId(Guid.Parse(id)));
    }

    private static Result Validate(string id)
    {
        var errors = new List<string>();

        if (!Guid.TryParse(id, out _))
        {
            errors.Add("Invalid InvitationId format.");
        }

        return errors.Any() ? Result.Failure(errors.ToArray()) : Result.Success();
    }
}