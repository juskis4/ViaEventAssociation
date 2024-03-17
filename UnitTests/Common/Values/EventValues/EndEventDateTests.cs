using ViaEventAssociation.Core.Domain.Aggregates.Events;
using Xunit;

namespace ViaEventAssociation.Core.Tests.Domain.Aggregates.Events;

public class EndEventDateTests
{
    // Success Scenarios

    //BUG: DateTime.Now
    [Fact]
    public void Create_ValidFutureEndDate_ReturnsSuccessResult()
    {
        // Arrange
        DateTime validEndDate = DateTime.Now.AddDays(2).Date.AddHours(15); 

        // Act
        var result = EndEventDate.Create(validEndDate);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(validEndDate, result.Data.EndDateTime);
    }

    //BUG: DateTime.Now
    [Fact]
    public void Create_EndDateTodayAtValidTime_ReturnsSuccessResult()
    {
        // Arrange
        DateTime validEndDate = DateTime.Now.Date.AddHours(21);

        // Act
        var result = EndEventDate.Create(validEndDate);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(validEndDate, result.Data.EndDateTime);
    }

    // Failure Scenarios

    [Fact]
    public void Create_EndDateInThePast_ReturnsFailureResult()
    {
        // Arrange
        DateTime pastEndDate = DateTime.Now.Subtract(TimeSpan.FromDays(1)); 

        // Act
        var result = EndEventDate.Create(pastEndDate);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("Event end date cannot be in the past.", result.Errors);
    }

    [Fact]
    public void Create_ValidDateButBefore8am_ReturnsFailureResult()
    {
        // Arrange
        DateTime invalidEndDate = DateTime.Now.AddDays(1).Date.AddHours(7); 

        // Act
        var result = EndEventDate.Create(invalidEndDate);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("Event end time must be between 08:00 AM and 01:00 AM (next day).", result.Errors);
    }

    [Fact]
    public void Create_ValidDateButAt1am_ReturnsFailureResult()
    {
        // Arrange
        DateTime invalidEndDate = DateTime.Now.AddDays(1).Date.AddHours(1); 

        // Act
        var result = EndEventDate.Create(invalidEndDate);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("Event end time must be between 08:00 AM and 01:00 AM (next day).", result.Errors);
    }

    // TODO: Tests for ensuring end date is after the start date 
    // TODO: Tests for event duration not exceeding 10 hours
}
