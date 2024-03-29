using ViaEventAssociation.Core.Domain.Aggregates.Events;
using ViaEventAssociation.Core.Domain.Aggregates.Events.ValueObjects;

namespace UnitTests.Features.Event.ReadiesEvent;
using Xunit;
public class ReadiesEventTests
{
    [Fact]
    public void ReadiesEvent_ValidDataAndDraftStatus_ReturnsSuccessResult()
    {
        // Arrange
        var viaEvent = EventFactory.CreateEventInDraftStatus().Data;
        // Act 
        var result = viaEvent.Readies();
        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(Status.Ready,viaEvent.Status);
    }
    
    [Fact]
    public void ReadiesEvent_ValidDataAndReadyStatus_ReturnsSuccessResult()
    {
        // Arrange
        var viaEvent = EventFactory.CreateEventInReadyStatus().Data;
        // Act 
        var result = viaEvent.Readies();
        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(Status.Ready,viaEvent.Status);
    }
    
    [Fact]
    public void ReadiesEvent_InValidDataAndDraftStatus_ReturnsFailureResult()
    {
        // Arrange
        var id = EventId.Create(Guid.NewGuid().ToString()).Data;
        var viaEvent = ViaEvent.Create(id).Data;
        // Act 
        var result = viaEvent.Readies();
        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("Cannot readies this event End Date is missing.",result.Errors);
        Assert.Contains("Cannot readies this event Start Date is missing.",result.Errors);
    }
    
    [Fact]
    public void ReadiesEvent_ValidDataAndCancelledStatus_ReturnsFailureResult()
    {
        // Arrange
        var viaEvent = EventFactory.CreateEventInCancelledStatus().Data;
        // Act 
        var result = viaEvent.Readies();
        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("The event must be in Draft status to readies. this Event is in Cancelled status",
            result.Errors);
    }
    
    [Fact]
    public void ReadiesEvent_ValidDataAndActiveStatus_ReturnsFailureResult()
    {
        // Arrange
        var viaEvent = EventFactory.CreateEventInActiveStatus().Data;
        // Act 
        var result = viaEvent.Readies();
        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("The event must be in Draft status to readies. this Event is in Active status",
            result.Errors);
    }
    
    [Fact]
    public void ReadiesEvent_DefaultTitleAndDraftStatus_ReturnsFailureResult()
    {
        // Arrange
        var id = EventId.Create(Guid.NewGuid().ToString()).Data;
        var viaEvent = ViaEvent.Create(id).Data;
        // Act 
        var result = viaEvent.Readies();
        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("Event title cannot be Working Title (default), please update the title",result.Errors);
    }
    
}