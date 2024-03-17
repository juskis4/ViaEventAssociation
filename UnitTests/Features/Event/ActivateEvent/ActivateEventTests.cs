using ViaEventAssociation.Core.Domain.Aggregates.Events;

namespace UnitTests.Features.Event.ActivateEvent;
using Xunit;
public class ActivateEventTests
{
        [Fact]
    public void ReadiesEvent_ValidDataAndDraftStatus_ReturnsSuccess()
    {
        // Arrange
        var viaEvent = EventFactory.CreateEventInDraftStatus().Data;
        // Act 
        var result = viaEvent.Activate();
        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(Status.Active,viaEvent.Status);
    }
    
    [Fact]
    public void ReadiesEvent_ValidDataAndActiveStatus_ReturnsSuccess()
    {
        // Arrange
        var viaEvent = EventFactory.CreateEventInActiveStatus().Data;
        // Act 
        var result = viaEvent.Activate();
        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(Status.Active,viaEvent.Status);
    }
    
    [Fact]
    public void ReadiesEvent_ValidDataAndReadyStatus_ReturnsSuccess()
    {
        // Arrange
        var viaEvent = EventFactory.CreateEventInReadyStatus().Data;
        // Act 
        var result = viaEvent.Activate();
        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(Status.Active,viaEvent.Status);
    }
    
    [Fact]
    public void ReadiesEvent_InValidDataAndDraftStatus_ReturnsFailure()
    {
        // Arrange
        var id = EventId.Create(Guid.NewGuid().ToString()).Data;
        var viaEvent = ViaEvent.Create(id).Data;
        // Act 
        var result = viaEvent.Activate();
        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("Cannot readies this event End Date is missing.",result.Errors);
        Assert.Contains("Cannot readies this event Start Date is missing.",result.Errors);
    }
    
    [Fact]
    public void ReadiesEvent_ValidDataAndCancelledStatus_ReturnsFailure()
    {
        // Arrange
        var viaEvent = EventFactory.CreateEventInCancelledStatus().Data;
        // Act 
        var result = viaEvent.Activate();
        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("The event must be in Draft status to readies. this Event is in Cancelled status",
            result.Errors);
    }
    

    
    [Fact]
    public void ReadiesEvent_DefaultTitleAndDraftStatus_ReturnsFailure()
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