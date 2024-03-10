

using ViaEventAssociation.Core.Domain.Common.Bases;

namespace ViaEventAssociation.Core.Domain.Aggregates.Guests;

public class Guest
{
    public FirstName firstName { get; set; }
    public LastName lastName { get; set; }
    public ViaEmail email { get; set; }
    public GuestId id { get; set; }

    public Guest(FirstName firstName, LastName lastName, ViaEmail email, GuestId id)
    {
        this.email = email;
        this.firstName = firstName;
        this.lastName = lastName;
        this.id = id;
    }
    
    
    
    // TODO: one UseCase of GuestPraticipate in an event.
    /*public Result Praticipate(EventId eventId)
    {
        var ViaEvent = getViaEvent(EventId);
        return result = ViaEvent.Praticipate(id);
    } */
    
}