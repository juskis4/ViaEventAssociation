using ViaEventAssociation.Core.Tools.OperationResult;
using ViaEventAssociation.Core.Domain.Aggregates.Events.ValueObjects;
using ViaEventAssociation.Core.Domain.Aggregates.Invitations.ValueObjects;
using ViaEventAssociation.Core.Domain.Aggregates.Guests.ValueObjects;

namespace ViaEventAssociation.Core.Domain.Aggregates.Invitations;

public class Invitation
{
    public InviteStatusEnum InviteStatus;
    public InvitationId InvitationId;

     public GuestId GuestId { get; private set; }
     public EventId EventId { get; private set; }

     private Invitation(InvitationId invitationId, GuestId guestId, EventId eventId, InviteStatusEnum inviteStatus)
    {
        InvitationId = invitationId;
        GuestId = guestId;
        EventId = eventId;
        InviteStatus = inviteStatus;
    }

    public static Result<Invitation> Create(GuestId guestId, EventId eventId) 
    {
        var invitationId = InvitationId.Create();
        if (!invitationId.IsSuccess)
        {
            return Result<Invitation>.Failure(invitationId.Errors.ToArray());
        }

        return Result<Invitation>.Success(
            new Invitation(invitationId.Data, guestId, eventId, InviteStatusEnum.Pending)
        );
    }
}

public enum InviteStatusEnum
{
    Pending,
    Accepted,
    Rejected
}

