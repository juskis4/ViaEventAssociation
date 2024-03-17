using ViaEventAssociation.Core.Domain.Aggregates.Events;
using ViaEventAssociation.Core.Tools.OperationResult;
using Xunit;
namespace UnitTests.Features.Event.SetMaxGuests;

public class SetMaxGuestsTests
{
    // Success Scenarios

   
    [Theory]
    [InlineData(5)] 
    [InlineData(10)]
    [InlineData(25)]
    [InlineData(50)]
    public void SetMaxGuest_ValidValues_DraftStatus_ReturnsSuccessResult(int maxGuests) // S1 S2
    {
        // Arrange
        var createEventResult  = EventFactory.CreateEventInDraftStatus(); 
        if (!createEventResult.IsSuccess)
        {
            Assert.Fail("Event creation failed: " + string.Join(", ", createEventResult.Errors)); 
            return; 
        }

        var existingEvent = createEventResult.Data; 
        
        // Act
        var setMaxGuest = existingEvent.setMaximumNumberGuests(maxGuests);

        // Assert
        Assert.True(setMaxGuest.IsSuccess); 
        Assert.Equal(maxGuests, existingEvent.Capacity.CapacityCount);
    }
    
    [Fact]
    public void SetMaxGuest_IncrementCapacity_ReadyStatus_ReturnsSuccessResult() // S1 S2
    {
        // Arrange
        var createEventResult  = EventFactory.CreateEventInActiveStatus(); 
        if (!createEventResult.IsSuccess)
        {
            Assert.Fail("Event creation failed: " + string.Join(", ", createEventResult.Errors)); 
            return; 
        }

        var existingEvent = createEventResult.Data; 
        
        // Act
         existingEvent.setMaximumNumberGuests(23);
         existingEvent.setMaximumNumberGuests(34);
        var setMaxGuest = existingEvent.setMaximumNumberGuests(45);

        // Assert
        Assert.True(setMaxGuest.IsSuccess); 
        Assert.Equal(45, existingEvent.Capacity.CapacityCount);
    }
    
    [Fact]
    public void SetMaxGuest_ReduceCapacity_ReadyStatus_ReturnsFailureResult() // F1
    {
        // Arrange
        var createEventResult  = EventFactory.CreateEventInActiveStatus(); 
        if (!createEventResult.IsSuccess)
        {
            Assert.Fail("Event creation failed: " + string.Join(", ", createEventResult.Errors)); 
            return; 
        }

        var existingEvent = createEventResult.Data;
        var eventCap = existingEvent.Capacity.CapacityCount;
        
        // Act
        var setMaxGuest = existingEvent.setMaximumNumberGuests(eventCap-1);

        // Assert
        Assert.False(setMaxGuest.IsSuccess); 
        Assert.Equal(eventCap,existingEvent.Capacity.CapacityCount);
        Assert.Contains("Capacity in active Event can only be increased", setMaxGuest.Errors);
    }
    
    [Fact]
    public void SetMaxGuest_CannotModify_CancelledStatus_ReturnsFailureResult() // F2
    {
        // Arrange
        var createEventResult  = EventFactory.CreateEventInCancelledStatus(); 
        if (!createEventResult.IsSuccess)
        {
            Assert.Fail("Event creation failed: " + string.Join(", ", createEventResult.Errors)); 
            return; 
        }

        var existingEvent = createEventResult.Data;
        var eventCap = existingEvent.Capacity.CapacityCount;
        
        // Act
        var setMaxGuest = existingEvent.setMaximumNumberGuests(30);

        // Assert
        Assert.False(setMaxGuest.IsSuccess); 
        Assert.Equal(eventCap,existingEvent.Capacity.CapacityCount);
        Assert.Contains("Event is Cancelled and cannot be modified", setMaxGuest.Errors);
    }
    
    [Fact]
    public void SetMaxGuest_LocationCapacity_DraftStatus_ReturnsFailureResult() // F3
    {
        // Arrange
        var createEventResult  = EventFactory.CreateEventInDraftStatus(); 
        if (!createEventResult.IsSuccess)
        {
            Assert.Fail("Event creation failed: " + string.Join(", ", createEventResult.Errors)); 
            return; 
        }

        var existingEvent = createEventResult.Data;
        var eventCap = existingEvent.Capacity.CapacityCount;
        //get room cap
        var roomCap = existingEvent;
        
        // Act
        var setMaxGuest = existingEvent.setMaximumNumberGuests(30);

        // Assert
        Assert.False(setMaxGuest.IsSuccess); 
        Assert.Equal(eventCap,existingEvent.Capacity.CapacityCount);
        Assert.Contains("Event Capacity cannot be higher than the location Capacity", setMaxGuest.Errors);
    }
    
    [Theory]
    [InlineData(4)]
    [InlineData(0)]
    [InlineData(-3)]
    public void SetMaxGuest_InvalidValues_DraftStatus_ReturnsFailureResult(int maxGuests) // F3
    {
        // Arrange
        var createEventResult  = EventFactory.CreateEventInCancelledStatus(); 
        if (!createEventResult.IsSuccess)
        {
            Assert.Fail("Event creation failed: " + string.Join(", ", createEventResult.Errors)); 
            return; 
        }

        var existingEvent = createEventResult.Data;
        var eventCap = existingEvent.Capacity.CapacityCount;
        //get room cap
        var roomCap = existingEvent;
        
        // Act
        var setMaxGuest = existingEvent.setMaximumNumberGuests(maxGuests);

        // Assert
        Assert.False(setMaxGuest.IsSuccess); 
        Assert.Equal(eventCap,existingEvent.Capacity.CapacityCount);
        Assert.Contains("Capacity cannot be less than 5", setMaxGuest.Errors);
    }
}