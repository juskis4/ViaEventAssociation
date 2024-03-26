using ViaEventAssociation.Core.Domain.Aggregates.Locations;
using Xunit;

namespace UnitTests.Common.Values.LocationValues;

public class LocationCapacityTest
{
    [Theory]
    [InlineData(10)]
    [InlineData(100)]
    [InlineData(999)]
    public void Create_ValidLocationCapacity_ReturnsSuccessResult(int capacity)
    {
        var locationCapacity = LocationCapacity.Create(capacity);
        
        Assert.True(locationCapacity.IsSuccess);
        Assert.Equal(capacity,locationCapacity.Data.Cap);
    }
    
    [Theory]
    [InlineData(-3)]
    [InlineData(-1)]
    [InlineData(-281)]
    public void Create_LocationCapacityLessThanZero_ReturnsFailureResult(int capacity)
    {
        var locationCapacity = LocationCapacity.Create(capacity);
        
        Assert.False(locationCapacity.IsSuccess);
        Assert.Contains("Location Capacity cannot be less than 0", locationCapacity.Errors);
        
    }
    
    [Theory]
    [InlineData(1000)]
    [InlineData(10000)]
    [InlineData(908898)]
    public void Create_LocationCapacityLMoreThanX_ReturnsFailureResult(int capacity)
    {
        var locationCapacity = LocationCapacity.Create(capacity);
        
        Assert.False(locationCapacity.IsSuccess);
        Assert.Contains("Location Capacity cannot exceed 999", locationCapacity.Errors);
        
    }
}