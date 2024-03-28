namespace UnitTests.Common.Values.LocationValues;
using ViaEventAssociation.Core.Domain.Aggregates.Locations;
using ViaEventAssociation.Core.Domain.Aggregates.Events.ValueObjects;

using Xunit;

public class LocatioAddressTests
{
    [Fact]
    public void Create_ValidLocationName_ReturnsSuccessResult()
    {
        var locationAddress = LocationName.Create("addressOne");
        Assert.True(locationAddress.IsSuccess);
        Assert.Equal("Addressone",locationAddress.Data.Name);
    }

    [Fact]
    public void Create_EmptyLocationName_ReturnsFailureResult()
    {
        var locationName = LocationName.Create("");
        
        Assert.False(locationName.IsSuccess);
        Assert.Contains("Location Name cannot be NULL or empty",locationName.Errors);
    }

    [Fact]
    public void Create_ShortLocationName_ReturnsFailureResult()
    {
        var locationName = LocationName.Create("i");
        
        Assert.False(locationName.IsSuccess);
        Assert.Contains("Location name cannot be less than 2 characters",locationName.Errors);
    }

    [Fact]
    public void Create_LongLocationName_ReturnsFailureResult()
    {
        var locationName = LocationName.Create("ThisIsTheLongestLocationAddressICanComeUpWithSoDealWithIt");
        
        Assert.False(locationName.IsSuccess);
        Assert.Contains("Location name cannot exceed 25 characters",locationName.Errors);
    }
}