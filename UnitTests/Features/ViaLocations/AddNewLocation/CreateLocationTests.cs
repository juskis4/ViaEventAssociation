using ViaEventAssociation.Core.Domain.Aggregates.Locations;
using Xunit;

namespace UnitTests.Features.ViaLocations.AddNewLocation;

public class CreateLocationTests
{
    [Fact]
    public void CreateLocation_ValidArguments_ResultSuccess()
    {
        var name = LocationName.Create("testName");
        var cap = LocationCapacity.Create(14);
        var address = Address.Create("Test Address");
        var id = LocationId.Create(1);

        var location = Location.Create(id.Data, address.Data, name.Data, cap.Data).Data;
        
        Assert.Equal(location.Id, id.Data);
        Assert.Equal(location.Address,address.Data);
        Assert.Equal(location.Capacity, cap.Data);
        Assert.Equal(location.Name,name.Data);
        Assert.True(location.Available);
        
    }
    
}