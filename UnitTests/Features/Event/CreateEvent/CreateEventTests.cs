using ViaEventAssociation.Core.Domain.Aggregates.Events;
using ViaEventAssociation.Core.Domain.Aggregates.Events.ValueObjects;
using Xunit;

namespace UnitTests.Features.Event.CreateEvent;

public class CreateEventTests
{

    [Fact]
    public void CreateEvent_GivenValidArguments_ReturnEvent()
    {
        // Arrange
        var eventId = EventId.Create();
        // Act 
        var viaEvent = ViaEvent.Create(eventId.Data);
        // Assert
        Assert.True(viaEvent.IsSuccess);
        Assert.Equal(5,viaEvent.Data.Capacity.CapacityCount);
        Assert.Equal("",viaEvent.Data.Description.DescriptionText);
        Assert.Equal("Working Title",viaEvent.Data.Title.Title);
        Assert.Equal(Status.Draft,viaEvent.Data.Status);
        Assert.False(viaEvent.Data.IsPublic);
    }
    
   
}