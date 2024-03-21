using ViaEventAssociation.Core.Domain.Aggregates.Events;
using Xunit;

namespace UnitTests.Features.Event.MakePrivate;

public class MakeEventPrivateTests
{
    [Fact]
    public void MakeEventPrivate_StatusIsDraft_ReturnsSuccess()
    {
        // Arrange  
        var viaEvent = EventFactory.CreateEventInDraftStatus().Data;

        // Act
        var newResult = viaEvent.MakeEventPrivate();
        // Assert 
        Assert.True(newResult.IsSuccess);
        Assert.False(viaEvent.IsPublic);
        Assert.Equal(Status.Draft, viaEvent.Status);
    }

    [Fact]
    public void MakeEventPublic_StatusIsReady_ReturnsSuccess()
    {
        // Arrange
        var viaEvent = EventFactory.CreateEventInReadyStatus().Data;
        // Act
        var result = viaEvent.MakeEventPrivate();
        // Assert 
        Assert.True(result.IsSuccess);
        Assert.Equal(Status.Draft, viaEvent.Status);
        Assert.False(viaEvent.IsPublic);
    }

    [Fact]
    public void MakeEventPrivate_StatusIsActive_ReturnsFailure()
    {
        // Arrange
        var viaEvent = EventFactory.CreateEventInActiveStatus().Data;
        // Act
        var result = viaEvent.MakeEventPrivate();
        // Assert 
        Assert.False(result.IsSuccess);
        Assert.False(viaEvent.IsPublic);
        Assert.Equal(Status.Active, viaEvent.Status);
        Assert.Contains("a Active event cannot be modified", result.Errors);
    }

    [Fact]
    public void MakeEventPrivate_StatusIsCancelled_ReturnsFailure()
    {
        // Arrange
        var viaEvent = EventFactory.CreateEventInCancelledStatus().Data;
        // Act
        var result = viaEvent.MakeEventPrivate();
        // Assert 
        Assert.False(result.IsSuccess);
        Assert.False(viaEvent.IsPublic);
        Assert.Equal(Status.Cancelled, viaEvent.Status);
        Assert.Contains("a Cancelled event cannot be modified", result.Errors);
    }
}