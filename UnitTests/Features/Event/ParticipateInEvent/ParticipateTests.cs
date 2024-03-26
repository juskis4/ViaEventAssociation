using ViaEventAssociation.Core.Domain.Aggregates.Events;
using ViaEventAssociation.Core.Domain.Aggregates.Guests.ValueObjects;

namespace UnitTests.Features.Event.ParticipateInEvent;
using Xunit;
public class ParticipateTests
{
    
    [Fact]
    public void Participate_SuccessScenario_ReturnsSuccessResult()
    {
        // Arrange
        var viaEvent = EventFactory.CreateEventInActiveStatus();
        var publicResult = viaEvent.Data.MakeEventPublic();
        var guestId = GuestId.Create();
        // Act
        var result = viaEvent.Data.Participate(guestId.Data);
        
        // Assert
        Assert.True(result.IsSuccess);
        Assert.True(publicResult.IsSuccess);
        Assert.Contains(guestId.Data, viaEvent.Data.GetGuests());
    }
    
    // F1
    [Fact]
    public void Participate_FailureScenarioF1a_ReturnsFailureResult()
    {
        // Arrange
        var viaEvent = EventFactory.CreateEventInDraftStatus();
        var guestId = GuestId.Create();
        // Act
        var result = viaEvent.Data.Participate(guestId.Data);
        
        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("You can only Join active events.", result.Errors);
        Assert.DoesNotContain(guestId.Data, viaEvent.Data.GetGuests());
    }
    
    [Fact]
    public void Participate_FailureScenarioF1b_ReturnsFailureResult()
    {
        // Arrange
        var viaEvent = EventFactory.CreateEventInReadyStatus();
        var guestId = GuestId.Create();
        // Act
        var result = viaEvent.Data.Participate(guestId.Data);
        
        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("You can only Join active events.", result.Errors);
        Assert.DoesNotContain(guestId.Data, viaEvent.Data.GetGuests());
    }
    
    [Fact]
    public void Participate_FailureScenarioF1c_ReturnsFailureResult()
    {
        // Arrange
        var viaEvent = EventFactory.CreateEventInCancelledStatus();
        var guestId = GuestId.Create();
        // Act
        var result = viaEvent.Data.Participate(guestId.Data);
        
        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("You can only Join active events.", result.Errors);
        Assert.DoesNotContain(guestId.Data, viaEvent.Data.GetGuests());
    }
    
    //F2
    [Fact]
    public void Participate_FailureScenarioF2_ReturnsFailureResult()
    {
        // Arrange
        var viaEvent = EventFactory.CreateEventInActiveStatus();
        var guestId1 = GuestId.Create();
        var guestId2 = GuestId.Create();
        var guestId3 = GuestId.Create();
        var guestId4 = GuestId.Create();
        var guestId5 = GuestId.Create();
        var guestId6 = GuestId.Create();
        var publicResult = viaEvent.Data.MakeEventPublic();
        var result1 = viaEvent.Data.Participate(guestId1.Data);
        var result2 = viaEvent.Data.Participate(guestId2.Data);
        var result3 = viaEvent.Data.Participate(guestId3.Data);
        var result4 = viaEvent.Data.Participate(guestId4.Data);
        var result5 = viaEvent.Data.Participate(guestId5.Data);

        
        // Act
        var result6 = viaEvent.Data.Participate(guestId6.Data);
        
        // Assert
        Assert.False(result6.IsSuccess);
        Assert.Contains("This Event is already at full capacity.", result6.Errors);
        Assert.DoesNotContain(guestId6.Data, viaEvent.Data.GetGuests());
    }
    
    //F3
    // TODO: How to create event in the past to test:???!
    /*
    [Fact]
    public void Participate_FailureScenarioF3_ReturnsFailureResult()
    {
        // Arrange
        var viaEvent = EventFactory.CreateEventInActiveStatus();
        var guestId = GuestId.Create();
        // Act
        var result = viaEvent.Data.Participate(guestId.Data);
        
        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("You can only Join active events.", result.Errors);
        Assert.DoesNotContain(guestId.Data, viaEvent.Data.GetGuests());

    }
    */
    
    //F4
    [Fact]
    public void Participate_FailureScenarioF4_ReturnsFailureResult()
    {
        // Arrange
        var viaEvent = EventFactory.CreateEventInDraftStatus();
        var guestId = GuestId.Create();
        var privateResult = viaEvent.Data.MakeEventPrivate();
        var readyResult = viaEvent.Data.Readies();
        // Act
        var result = viaEvent.Data.Participate(guestId.Data);
        
        // Assert
        Assert.True(privateResult.IsSuccess);
        Assert.True(readyResult.IsSuccess);
        Assert.False(result.IsSuccess);
        Assert.Contains("Cannot join a non-public event.", result.Errors);
        Assert.DoesNotContain(guestId.Data, viaEvent.Data.GetGuests());
    }
    
    //F5
    [Fact]
    public void Participate_FailureScenarioF5_ReturnsFailureResult()
    {
        // Arrange
        var viaEvent = EventFactory.CreateEventInActiveStatus();
        var publicResult = viaEvent.Data.MakeEventPublic();
        var guestId = GuestId.Create();
        var result = viaEvent.Data.Participate(guestId.Data);
        // Act
        var newResult = viaEvent.Data.Participate(guestId.Data);
        
        // Assert
        Assert.True(result.IsSuccess);
        Assert.True(publicResult.IsSuccess);
        Assert.False(newResult.IsSuccess);
        Assert.Contains("You are already registered as a guest for this event.", newResult.Errors);
        Assert.Contains(guestId.Data, viaEvent.Data.GetGuests());
    }
}