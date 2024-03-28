using Xunit;

namespace UnitTests.Features.ViaLocations.SetMaxCapacity;

public class SetMaxCapacityTests
{
    [Theory]
    [InlineData(5)]
    [InlineData(100)]
    [InlineData(250)]
    [InlineData(999)]
    public void SetMaxCap_ValidValues_ResultSuccess(int capacity)
    {
        var newLocationResult =  LocationFactory.CreateAvailableLocation();
        if (!newLocationResult.IsSuccess)
        {
            Assert.Fail("Location creation failed: " + string.Join(", ", newLocationResult.Errors)); 
            return;
        }

        var result = newLocationResult.Data.SetMaximumNumberOfPeople(capacity);
        
        Assert.True(result.IsSuccess);
        Assert.Equal(capacity,newLocationResult.Data.Capacity.Cap);
    }
    
    [Theory]
    [InlineData(-1)]
    [InlineData(-7)]
    [InlineData(12000)]
    public void SetMaxCap_InvalidValues_ResultFailure(int capacity)
    {
        var newLocationResult =  LocationFactory.CreateAvailableLocation();
        if (!newLocationResult.IsSuccess)
        {
            Assert.Fail("Location creation failed: " + string.Join(", ", newLocationResult.Errors)); 
            return;
        }

        int oldCap = newLocationResult.Data.Capacity.Cap;
        var result = newLocationResult.Data.SetMaximumNumberOfPeople(capacity);
        
        Assert.False(result.IsSuccess);
        Assert.Equal(oldCap,newLocationResult.Data.Capacity.Cap);
    }
}