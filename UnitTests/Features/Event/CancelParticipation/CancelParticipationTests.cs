using UnitTests.Features.Event;
using ViaEventAssociation.Core.Domain.Aggregates.Events;
using ViaEventAssociation.Core.Domain.Aggregates.Guests.ValueObjects;

namespace UnitTests.Features.CancelParticipation;
using Xunit;
public class CancelParticipationTests
{

    private ViaEvent CreateEvent()
    {
        var viaEvent = EventFactory.CreateEventInActiveStatus();
        var publicResult = viaEvent.Data.MakeEventPublic();
        return viaEvent.Data;
    }

    // S1
    [Fact]
    public void CancelParticipation_SuccessScenario1_ReturnsSuccessResult()
    {
        // Arrange
        var viaEvent = CreateEvent();
        var guestId = GuestId.Create();
        var participate = viaEvent.Participate(guestId.Data);
        
        // Act
        var cancelResult = viaEvent.CancelParticipation(guestId.Data);
        
        // Assert
        Assert.True(cancelResult.IsSuccess);
        Assert.DoesNotContain(guestId.Data, viaEvent.GetGuests());
    }
    
    // S2
    [Fact]
    public void CancelParticipation_SuccessScenario2_ReturnsSuccessResult()
    {
        // Arrange
        var viaEvent = CreateEvent();
        var guestId = GuestId.Create();
        // Act
        var cancelResult = viaEvent.CancelParticipation(guestId.Data);
        
        // Assert
        Assert.True(cancelResult.IsSuccess);
        Assert.DoesNotContain(guestId.Data, viaEvent.GetGuests());
    }
    
    //F1
    [Fact]
    public void CancelParticipation_FailureScenario1_ReturnsFailureResult()
    {
        // Arrange
        var viaEvent = EventFactory.CreatePublicEventInActiveStatusInPast().Data;
        var guestId = GuestId.Create();
        viaEvent.SetGuest(guestId.Data);
        // Act
        var cancelResult = viaEvent.CancelParticipation(guestId.Data);
        
        // Assert
        Assert.False(cancelResult.IsSuccess);
        Assert.Contains("Cannot cancel your participation of past or ongoing events.", cancelResult.Errors);
        Assert.Contains(guestId.Data, viaEvent.GetGuests());
    }

}