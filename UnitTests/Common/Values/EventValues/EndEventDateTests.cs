using ViaEventAssociation.Core.Domain.Aggregates.Events;
using ViaEventAssociation.Core.Domain.Aggregates.Events.ValueObjects;
using Xunit;

namespace ViaEventAssociation.Core.Tests.Domain.Aggregates.Events;

public class EndEventDateTests
{
    // Success Scenarios

    [Fact]
    public void Create_ValidFutureEndDate_ReturnsSuccessResult()
    {
        // Arrange
        DateTime validEndDate = DateTime.Today.AddDays(2).Date.AddHours(15); 

        // Act
        var result = EndEventDate.Create(validEndDate);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(validEndDate, result.Data.Date);
    }

    [Fact]
    public void Create_EndDateTodayAtValidTime_ReturnsSuccessResult()
    {
        // Arrange
        DateTime validEndDate = DateTime.Today.Date.AddHours(21);

        // Act
        var result = EndEventDate.Create(validEndDate);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(validEndDate, result.Data.Date);
    }

    // Failure Scenarios
    

    [Fact]
    public void Create_ValidDateButBefore8am_ReturnsFailureResult()
    {
        // Arrange
        DateTime invalidEndDate = DateTime.Today.AddDays(1).Date.AddHours(7); 

        // Act
        var result = EndEventDate.Create(invalidEndDate);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("Event end time must fall within room usage hours (08:00 AM - 01:00 AM).", result.Errors);
    }

    [Fact]
    public void Create_ValidDateButAt1am_ReturnsFailureResult()
    {
        // Arrange
        DateTime invalidEndDate = DateTime.Today.AddDays(1).Date.AddHours(1).AddMinutes(1); 

        // Act
        var result = EndEventDate.Create(invalidEndDate);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("Event end time must fall within room usage hours (08:00 AM - 01:00 AM).", result.Errors);
    }
}
