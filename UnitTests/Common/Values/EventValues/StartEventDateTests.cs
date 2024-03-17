using ViaEventAssociation.Core.Domain.Aggregates.Events;
using Xunit;

namespace ViaEventAssociation.Core.Tests.Domain.Aggregates.Events;

public class StartEventDateTests
{
    // Success Scenarios 

    [Fact]
    public void Create_ValidFutureDate_ReturnsSuccessResult()
    {
        // Arrange
        DateTime validDate = DateTime.Now.AddDays(2).Date.AddHours(10);

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
        var frozenNow = DateTime.Now;
        // When ever the time is adding 8 hours will be valid.
        DateTime validDate = frozenNow.AddHours(8);

        // Act
        var result = StartEventDate.Create(validDate);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(validDate, result.Data.Date);
    }


    // Failure Scenarios 

    [Fact]
    public void Create_DateInThePast_ReturnsFailureResult()
    {
        // Arrange
        DateTime pastDate = DateTime.Now.Subtract(TimeSpan.FromDays(1));

        // Act
        var result = StartEventDate.Create(pastDate);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("Event start date cannot be in the past.", result.Errors);
    }

    [Fact]
    public void Create_ValidDateButBefore8am_ReturnsFailureResult()
    {
        // Arrange
        DateTime invalidDate = DateTime.Now.AddDays(1).Date.AddHours(7); 

        // Act
        var result = StartEventDate.Create(invalidDate);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("Event start time must be between 08:00 AM and 11:59 PM.", result.Errors);
    }

    [Fact]
    public void Create_ValidDateButAtMidnight_ReturnsFailureResult()
    {
         // Arrange
        DateTime invalidDate = DateTime.Now.AddDays(1).Date;

        // Act
        var result = StartEventDate.Create(invalidDate);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("Event start time must be between 08:00 AM and 11:59 PM.", result.Errors);
    }
}
