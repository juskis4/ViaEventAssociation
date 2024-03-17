using Xunit;

namespace UnitTests.Features.ViaLocations.SetMaxCapacity;

public class SetMaxCapacityTests
{
    [Theory]
    [InlineData(10)]
    [InlineData(100)]
    [InlineData(900)]
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
        Assert.Equal(capacity,newLocationResult.Data.capacity.Cap);
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

        int oldCap = newLocationResult.Data.capacity.Cap;
        var result = newLocationResult.Data.SetMaximumNumberOfPeople(capacity);
        
        Assert.False(result.IsSuccess);
        Assert.Equal(oldCap,newLocationResult.Data.capacity.Cap);
    }
}