using System.Diagnostics;
using ViaEventAssociation.Core.Domain.Aggregates.Guests;
using ViaEventAssociation.Core.Domain.Common.Bases;
using Xunit;

namespace UnitTests.Common.Factories;

public class GuestCreationTests
{

    [Fact]
    public void CreateGuest_WithSuccess()
    {
        // Arrange
        var firstname = FirstName.Create("John");
        var lastname = LastName.Create("wick");
        var email = ViaEmail.Create("Johnwick@via.dk");
        var id = GuestId.Create(112233);
        // Act 
        var guest = new Guest(firstname.Data, lastname.Data, email.Data, id.Data);
        
        // Assert
        Assert.Equal(firstname.Data.Name,guest.firstName.Name);
        Assert.Equal(lastname.Data.Name,guest.lastName.Name);
        Assert.Equal(email.Data.Email,guest.email.Email);
        Assert.Equal(id.Data.id,guest.id.id);

    }
    
}