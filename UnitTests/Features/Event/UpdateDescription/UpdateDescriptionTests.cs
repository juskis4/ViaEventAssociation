using ViaEventAssociation.Core.Domain.Aggregates.Events;
using Xunit;

namespace UnitTests.Features.Event
{
    public class UpdateDescriptionTests
    {
        // Success Scenarios

        [Fact] 
        public void UpdateDescription_ValidDescription_DraftStatus_ReturnsSuccessResult() // S1
        {
            // Arrange
            var createEventResult  = EventFactory.CreateEventInDraftStatus(); 
            if (!createEventResult.IsSuccess)
            {
                Assert.Fail("Event creation failed: " + string.Join(", ", createEventResult.Errors)); 
                return; 
            }

            var existingEvent = createEventResult.Data; 
            var newDescription = "Nullam tempor lacus nisl, eget tempus quam maximus malesuada. Morbi faucibus sed neque vitae euismod. Vestibulum non purus vel justo ornare vulputate."; 
                
            // Act
            var updateResult = existingEvent.UpdateDescription(newDescription);

            // Assert
            Assert.True(updateResult.IsSuccess); 
            Assert.Equal(newDescription, existingEvent.Description.DescriptionText);
        }

        [Fact] 
        public void UpdateDescription_EmptyDescription_DraftStatus_ReturnsSuccessResult() // S2
        {
            // Arrange
            var createEventResult  = EventFactory.CreateEventInDraftStatus(); 
            if (!createEventResult.IsSuccess)
            {
                Assert.Fail("Event creation failed: " + string.Join(", ", createEventResult.Errors)); 
                return; 
            }

            var existingEvent = createEventResult.Data; 
            var newDescription = ""; 

            // Act
            var result = existingEvent.UpdateDescription(newDescription);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("", existingEvent.Description.DescriptionText); 
        }

        [Fact] 
        public void UpdateDescription_ValidDescription_ReadyStatus_ReturnsSuccessResult() // S3
        {
            // Arrange
            var createEventResult  = EventFactory.CreateEventInReadyStatus(); 
            if (!createEventResult.IsSuccess)
            {
                Assert.Fail("Event creation failed: " + string.Join(", ", createEventResult.Errors)); 
                return; 
            }
            
            var existingEvent = createEventResult.Data; 
            var newDescription = "Updated description"; 

            // Act
            var result = existingEvent.UpdateDescription(newDescription);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(newDescription, existingEvent.Description.DescriptionText);
            Assert.Equal(Status.Draft, existingEvent.Status); 
        }

        // Failure Scenarios

        [Fact]
        public void UpdateDescription_TooLongDescription_DraftStatus_ReturnsFailureResult() // F1
        {
            // Arrange
            var createEventResult  = EventFactory.CreateEventInDraftStatus(); 
            if (!createEventResult.IsSuccess)
            {
                Assert.Fail("Event creation failed: " + string.Join(", ", createEventResult.Errors)); 
                return; 
            }
            var existingEvent = createEventResult.Data; 
            var newDescription = new string('X', 251); 

            // Act
            var result = existingEvent.UpdateDescription(newDescription);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("Description cannot exceed 250 characters.", result.Errors);
        }

        [Fact]
        public void UpdateDescription_CancelledEvent_ReturnsFailureResult() // F2
        {
            // Arrange
            var createEventResult  = EventFactory.CreateEventInCancelledStatus(); 
            if (!createEventResult.IsSuccess)
            {
                Assert.Fail("Event creation failed: " + string.Join(", ", createEventResult.Errors)); 
                return; 
            }

            var existingEvent = createEventResult.Data;
            var newDescription = "Updated description"; 

            // Act
            var result = existingEvent.UpdateDescription(newDescription);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("Event in status Cancelled cannot be modified.", result.Errors);
        }

        [Fact]
        public void UpdateDescription_ActiveEvent_ReturnsFailureResult() // F3
        {
            // Arrange
            var createEventResult  = EventFactory.CreateEventInActiveStatus(); 
            if (!createEventResult.IsSuccess)
            {
                Assert.Fail("Event creation failed: " + string.Join(", ", createEventResult.Errors)); 
                return; 
            }

            var existingEvent = createEventResult.Data;
            var newDescription = "Updated description"; 

            // Act
            var result = existingEvent.UpdateDescription(newDescription);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains("Event in status Active cannot be modified.", result.Errors);
        }
    }
}
