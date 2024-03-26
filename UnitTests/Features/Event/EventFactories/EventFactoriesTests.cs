using ViaEventAssociation.Core.Domain.Aggregates.Events;

namespace UnitTests.Features.Event.EventFactories;
using Xunit;
public class EventFactoriesTests
{

    [Fact]
    public void CreateEventInDraftStatus()
    {
        var viaEvent = EventFactory.CreateEventInDraftStatus();
        
        Assert.True(viaEvent.IsSuccess);
        Assert.Equal(Status.Draft, viaEvent.Data.Status);
    }
    
    [Fact]
    public void CreateEventInReadyStatus()
    {
        var viaEvent = EventFactory.CreateEventInReadyStatus();
        
        Assert.True(viaEvent.IsSuccess);
        Assert.Equal(Status.Ready, viaEvent.Data.Status);
    }
    
    [Fact]
    public void CreateEventInCancelledStatus()
    {
        var viaEvent = EventFactory.CreateEventInCancelledStatus();
        
        Assert.True(viaEvent.IsSuccess);
        Assert.Equal(Status.Cancelled, viaEvent.Data.Status);
    }
    
    [Fact]
    public void CreateEventInActiveStatus()
    {
        var viaEvent = EventFactory.CreateEventInActiveStatus();
        
        Assert.True(viaEvent.IsSuccess);
        Assert.Equal(Status.Active, viaEvent.Data.Status);
    }
    
        
    [Fact]
    public void CreatePublicEventInActiveStatusInPast()
    {
        var viaEvent = EventFactory.CreatePublicEventInActiveStatusInPast();
        
        Assert.True(viaEvent.IsSuccess);
        Assert.True(viaEvent.Data.IsPublic);
        Assert.True(DateTime.Now > viaEvent.Data.StartDate.Date);
        Assert.Equal(Status.Active, viaEvent.Data.Status);
    }
    
}