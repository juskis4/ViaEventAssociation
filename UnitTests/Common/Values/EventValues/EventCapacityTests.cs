using ViaEventAssociation.Core.Domain.Aggregates.Events;
using ViaEventAssociation.Core.Domain.Aggregates.Events.ValueObjects;
using Xunit;

namespace ViaEventAssociation.Core.Tests.Domain.Aggregates.Events;

public class EventCapacityTests
{
    // Success Scenarios 

    [Theory]
    [InlineData(5)]  
    [InlineData(25)]
    [InlineData(50)] 
    public void Create_ValidCapacity_ReturnsSuccessResult(int capacityCount)
    {
        // Act
        var result = Capacity.Create(capacityCount);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(capacityCount, result.Data.CapacityCount);
    }

    // Failure Scenarios

    [Theory]
    [InlineData(4)] 
    [InlineData(0)]
    [InlineData(-10)]
    public void Create_CapacityLessThan5_ReturnsFailureResult(int capacityCount)
    {
        // Act
        var result = Capacity.Create(capacityCount);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("Capacity cannot be less than 5", result.Errors);
    }

    [Theory]
    [InlineData(51)] 
    [InlineData(60)]
    [InlineData(100)] 
    public void Create_CapacityGreaterThan50_ReturnsFailureResult(int capacityCount)
    {
        // Act
        var result = Capacity.Create(capacityCount);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("Capacity cannot be greater than 50", result.Errors);
    }
}
