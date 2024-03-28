

using ViaEventAssociation.Core.Domain.Aggregates.Guests.ValueObjects;
using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.Guests;

public class Guest
{
    public FirstName firstName { get; set; }
    public LastName lastName { get; set; }
    public ViaEmail email { get; set; }
    public GuestId id { get; set; }

    private Guest(FirstName firstName, LastName lastName, ViaEmail email, GuestId id)
    {
        this.email = email;
        this.firstName = firstName;
        this.lastName = lastName;
        this.id = id;
    }

    public static Result<Guest> Create(FirstName firstName, LastName lastName, ViaEmail email, GuestId id)
    {
        return Result<Guest>.Success(new Guest(firstName, lastName, email, id));
    }
    
}