using System.Diagnostics;
using ViaEventAssociation.Core.Domain.Aggregates.Guests;
using ViaEventAssociation.Core.Domain.Aggregates.Guests.ValueObjects;
using Xunit;

namespace UnitTests.Common.Factories;

public class GuestCreationTests
{

    [Fact]
    public void CreateGuest_WithSuccess()
    {
        var firstname = FirstName.Create("john");
        var lastname = LastName.Create("wick");
        var email = ViaEmail.Create("jon@via.dk");
        var id = GuestId.Create();
        if (firstname.IsSuccess && lastname.IsSuccess && email.IsSuccess && id.IsSuccess)
        {
            var guest = Guest.Create(firstname.Data, lastname.Data, email.Data, id.Data);
            Assert.True(guest.IsSuccess);
            Assert.Equal("John",guest.Data.firstName.Name);
            Assert.Equal("Wick",guest.Data.lastName.Name);
            Assert.Equal(id.Data.Id,guest.Data.id.Id);
            Assert.Equal(email.Data.Email,guest.Data.email.Email);
        }
        else
        {
            Assert.True(false,"Guest creation failed");
        }

    }
    
}