using ViaEventAssociation.Core.Domain.Aggregates.Events;
using ViaEventAssociation.Core.Domain.Aggregates.Events.ValueObjects;
using Xunit;

namespace ViaEventAssociation.Core.Tests.Domain.Aggregates.Events;

public class StartEventDateTests
{
    // Success Scenarios 

    [Fact]
    public void Create_ValidFutureDate_ReturnsSuccessResult()
    {
        // Arrange
        DateTime validDate = DateTime.Today.AddDays(2).Date.AddHours(10);

        // Act
        var result = StartEventDate.Create(validDate);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(validDate, result.Data.Date);
    }

    [Fact]
    public void Create_DateTodayAtValidTime_ReturnsSuccessResult()
    {
        // Arrange
        DateTime validDate = DateTime.Today.AddHours(15);

        // Act
        var result = StartEventDate.Create(validDate);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(validDate, result.Data.Date);
    }


    // Failure Scenarios 

    [Fact]
    public void Create_ValidDateButBefore8am_ReturnsFailureResult()
    {
        // Arrange
        DateTime invalidDate = DateTime.Today.AddDays(1).Date.AddHours(7); 

        // Act
        var result = StartEventDate.Create(invalidDate);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("Event start time must be after 08:00 AM", result.Errors);
    }

    [Fact]
    public void Create_ValidDateButAtMidnight_ReturnsFailureResult()
    {
         // Arrange
        DateTime invalidDate = DateTime.Today.AddDays(1).AddHours(4);

        // Act
        var result = StartEventDate.Create(invalidDate);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("Event start time must be after 08:00 AM", result.Errors);
    }
}
