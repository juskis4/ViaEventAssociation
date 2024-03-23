using ViaEventAssociation.Core.Domain.Aggregates.Events;
using Xunit;

namespace UnitTests.Features.Event;
public class UpdateEventTimesTests
{
    // Success Scenarios
    [Theory]
    [InlineData("2024/08/25 19:00", "2024/08/25 23:59")]
    [InlineData("2024/08/25 12:00", "2024/08/25 16:30")]
    [InlineData("2024/08/25 08:00", "2024/08/25 12:15")]
    [InlineData("2024/08/25 10:00", "2024/08/25 20:00")]
    [InlineData("2024/08/25 13:00", "2024/08/25 23:00")]
    [InlineData("2024/08/25 19:00", "2024/08/26 00:55")]
    public void Update_ValidTimes_ReturnsSuccessResult(string start, string end)       // S1 + S2
    {
        // Arrange
        var existingEvent = EventFactory.CreateEventInDraftStatus();

        // Create StartEventDate 
        var newStartDateResult = StartEventDate.Create(DateTime.Parse(start));
        if (!newStartDateResult.IsSuccess)
        {
            Assert.Fail("Failed to create StartEventDate: " + string.Join(", ", newStartDateResult.Errors));
            return;
        }

        // Create EndEventDate
        var newEndDateResult = EndEventDate.Create(DateTime.Parse(end));
        if (!newEndDateResult.IsSuccess)
        {
            Assert.Fail("Failed to create EndEventDate: " + string.Join(", ", newEndDateResult.Errors));
            return;
        }

        // Act
        var updateResult = existingEvent.Data.UpdateEventTimes(newStartDateResult.Data, newEndDateResult.Data);

        // Assert
        Assert.True(updateResult.IsSuccess, "Event times update failed: " + string.Join(", ", updateResult.Errors));
        Assert.Equal(newStartDateResult.Data.Date, existingEvent.Data.StartDate.Date);
        Assert.Equal(newEndDateResult.Data.Date, existingEvent.Data.EndDate.Date);
    }

    [Theory]
    [InlineData("2024/08/25 19:00", "2024/08/25 23:59")]
    [InlineData("2024/08/25 12:00", "2024/08/25 16:30")]
    [InlineData("2024/08/25 08:00", "2024/08/25 12:15")]
    [InlineData("2024/08/25 10:00", "2024/08/25 20:00")]
    [InlineData("2024/08/25 13:00", "2024/08/25 23:00")]
    [InlineData("2024/08/25 19:00", "2024/08/26 00:55")]
    public void Update_ReadyEvent_ValidTimes_ReturnsSuccessResult(string start, string end)      // S3
    {
        // Arrange
        var existingEvent = EventFactory.CreateEventInReadyStatus();

        // Create StartEventDate 
        var newStartDateResult = StartEventDate.Create(DateTime.Parse(start));
        if (!newStartDateResult.IsSuccess)
        {
            Assert.Fail("Failed to create StartEventDate: " + string.Join(", ", newStartDateResult.Errors));
            return;
        }

        // Create EndEventDate 
        var newEndDateResult = EndEventDate.Create(DateTime.Parse(end));
        if (!newEndDateResult.IsSuccess)
        {
            Assert.Fail("Failed to create EndEventDate: " + string.Join(", ", newEndDateResult.Errors));
            return;
        }

        // Act
        var updateResult = existingEvent.Data.UpdateEventTimes(newStartDateResult.Data, newEndDateResult.Data);

        // Assert
        Assert.True(updateResult.IsSuccess, "Event times update failed: " + string.Join(", ", updateResult.Errors));
        Assert.Equal(Status.Draft, existingEvent.Data.Status);
        Assert.Equal(newStartDateResult.Data.Date, existingEvent.Data.StartDate.Date);
        Assert.Equal(newEndDateResult.Data.Date, existingEvent.Data.EndDate.Date);
    }


    // Failure Scenarios

    [Theory]
    [InlineData("2024/08/26 19:00", "2024/08/25 01:00")]
    [InlineData("2024/08/26 19:00", "2024/08/25 23:59")]
    [InlineData("2024/08/27 12:00", "2024/08/25 16:30")]
    [InlineData("2024/08/01 08:00", "2024/07/31 12:15")]
    public void Update_StartDateAfterEndDate_ReturnsFailureResult(string start, string end)        // F1
    {
        // Arrange
        var existingEvent = EventFactory.CreateEventInDraftStatus();

        // Create StartEventDate 
        var newStartDateResult = StartEventDate.Create(DateTime.Parse(start));
        if (!newStartDateResult.IsSuccess)
        {
            Assert.Fail("Failed to create StartEventDate: " + string.Join(", ", newStartDateResult.Errors));
            return;
        }

        // Create EndEventDate 
        var newEndDateResult = EndEventDate.Create(DateTime.Parse(end));
        if (!newEndDateResult.IsSuccess)
        {
            Assert.Fail("Failed to create EndEventDate: " + string.Join(", ", newEndDateResult.Errors));
            return;
        }

        // Act
        var updateResult = existingEvent.Data.UpdateEventTimes(newStartDateResult.Data, newEndDateResult.Data);

        // Assertions
        Assert.False(updateResult.IsSuccess);
        Assert.Contains("Start date cannot be after end date", updateResult.Errors);
    }

    [Theory]
    [InlineData("2024/08/26 19:00", "2024/08/26 14:00")]
    [InlineData("2024/08/26 16:00", "2024/08/26 00:00")]
    [InlineData("2024/08/26 19:00", "2024/08/26 18:59")]
    [InlineData("2024/08/26 12:00", "2024/08/26 10:10")]
    [InlineData("2024/08/26 08:00", "2024/08/26 00:30")]
    public void Update_StartTimeAfterEndTime_ReturnsFailureResult(string start, string end)        // F2
    {
        // Arrange
        var existingEvent = EventFactory.CreateEventInDraftStatus();

        // Create StartEventDate 
        var newStartDateResult = StartEventDate.Create(DateTime.Parse(start));
        if (!newStartDateResult.IsSuccess)
        {
            Assert.Fail("Failed to create StartEventDate: " + string.Join(", ", newStartDateResult.Errors));
            return;
        }

        // Create EndEventDate 
        var newEndDateResult = EndEventDate.Create(DateTime.Parse(end));
        if (!newEndDateResult.IsSuccess)
        {
            Assert.Fail("Failed to create EndEventDate: " + string.Join(", ", newEndDateResult.Errors));
            return;
        }

        // Act
        var updateResult = existingEvent.Data.UpdateEventTimes(newStartDateResult.Data, newEndDateResult.Data);

        // Assertions
        Assert.False(updateResult.IsSuccess);
        Assert.Contains("Start time must be before end time on the same day", updateResult.Errors);
    }

    [Theory]
    [InlineData("2024/08/26 14:00", "2024/08/26 14:50")]
    [InlineData("2024/08/26 18:00", "2024/08/26 18:59")]
    [InlineData("2024/08/26 12:00", "2024/08/26 12:30")]
    [InlineData("2024/08/26 08:00", "2024/08/26 08:00")]
    [InlineData("2024/08/25 23:30", "2024/08/26 00:15")]
    [InlineData("2024/08/30 23:01", "2024/08/31 00:00")]
    [InlineData("2024/08/30 23:59", "2024/08/31 00:01")]
    public void Update_DurationLessThanOneHour_ReturnsFailureResult(string start, string end)      // F3 and F4
    {
        // Arrange
        var existingEvent = EventFactory.CreateEventInDraftStatus();

        // Create StartEventDate 
        var newStartDateResult = StartEventDate.Create(DateTime.Parse(start));
        if (!newStartDateResult.IsSuccess)
        {
            Assert.Fail("Failed to create StartEventDate: " + string.Join(", ", newStartDateResult.Errors));
            return;
        }

        // Create EndEventDate 
        var newEndDateResult = EndEventDate.Create(DateTime.Parse(end));
        if (!newEndDateResult.IsSuccess)
        {
            Assert.Fail("Failed to create EndEventDate: " + string.Join(", ", newEndDateResult.Errors));
            return;
        }

        // Act
        var updateResult = existingEvent.Data.UpdateEventTimes(newStartDateResult.Data, newEndDateResult.Data);

        // Assert
        Assert.False(updateResult.IsSuccess);
        Assert.Contains("Event duration must be between 1 and 10 hours", updateResult.Errors);
    }

    [Theory]
    [InlineData("2024/08/25 07:50")]
    [InlineData("2024/08/25 07:59")]
    [InlineData("2024/08/25 01:01")]
    [InlineData("2024/08/25 05:59")]
    [InlineData("2024/08/25 00:59")]

    public void Update_StartTimeBefore8AM_ReturnsFailureResult(string start)       // F5
    {
        // Arrange
        var existingEvent = EventFactory.CreateEventInDraftStatus();

        // Create StartEventDate
        var newStartDateResult = StartEventDate.Create(DateTime.Parse(start));

        // Assert 
        Assert.False(newStartDateResult.IsSuccess);
        Assert.Contains("Event start time must be after 08:00 AM", newStartDateResult.Errors);

    }

    [Theory]
    [InlineData("2024/08/30 23:59", "2024/08/31 08:01")]
    public void Update_RoomsAreClosed_ReturnsFailureResult(string start, string end)      // F6 and F11
    {
        // Arrange
        var existingEvent = EventFactory.CreateEventInDraftStatus();

        // Create StartEventDate 
        var newStartDateResult = StartEventDate.Create(DateTime.Parse(start));
        if (!newStartDateResult.IsSuccess)
        {
            Assert.Fail("Failed to create StartEventDate: " + string.Join(", ", newStartDateResult.Errors));
            return;
        }

        // Create EndEventDate 
        var newEndDateResult = EndEventDate.Create(DateTime.Parse(end));
        if (!newEndDateResult.IsSuccess)
        {
            Assert.Fail("Failed to create EndEventDate: " + string.Join(", ", newEndDateResult.Errors));
            return;
        }

        // Act
        var updateResult = existingEvent.Data.UpdateEventTimes(newStartDateResult.Data, newEndDateResult.Data);

        // Assert
        Assert.False(updateResult.IsSuccess);
        Assert.Contains("Rooms are not usable between 01:01 AM and 07:59 AM", updateResult.Errors);
    }

    [Theory]
    [InlineData("2024/08/24 23:50", "2024/08/25 01:01")]
    [InlineData("2024/08/24 22:00", "2024/08/25 07:59")]
    [InlineData("2024/08/30 23:00", "2024/08/31 02:30")]
    public void Update_RoomsAreClosed_EndEventDate_ReturnsFailureResult(string start,string end)       // F6 and F11
    {
        // Create StartEventDate 
        var newStartDateResult = StartEventDate.Create(DateTime.Parse(start));
        if (!newStartDateResult.IsSuccess)
        {
            Assert.Fail("Failed to create StartEventDate: " + string.Join(", ", newStartDateResult.Errors));
            return;
        }

        // Create StartEventDate
        var newEndDateResult = EndEventDate.Create(DateTime.Parse(end));

        // Assert 
        Assert.False(newEndDateResult.IsSuccess);
        Assert.Contains("Event end time must fall within room usage hours (08:00 AM - 01:00 AM).", newEndDateResult.Errors);

    }

    [Theory]
    [InlineData("2024/08/25 12:00", "2024/08/25 16:30")]
    [InlineData("2024/08/25 08:00", "2024/08/25 12:15")]
    public void Update_ActiveEvent_ReturnsFailureResult(string start, string end)      // F7
    {
        // Arrange
        var existingEvent = EventFactory.CreateEventInActiveStatus();

        // Create StartEventDate 
        var newStartDateResult = StartEventDate.Create(DateTime.Parse(start));
        if (!newStartDateResult.IsSuccess)
        {
            Assert.Fail("Failed to create StartEventDate: " + string.Join(", ", newStartDateResult.Errors));
            return;
        }

        // Create EndEventDate 
        var newEndDateResult = EndEventDate.Create(DateTime.Parse(end));
        if (!newEndDateResult.IsSuccess)
        {
            Assert.Fail("Failed to create EndEventDate: " + string.Join(", ", newEndDateResult.Errors));
            return;
        }

        // Act
        var updateResult = existingEvent.Data.UpdateEventTimes(newStartDateResult.Data, newEndDateResult.Data);

        // Assert
        Assert.False(updateResult.IsSuccess);
        Assert.Contains("Event cannot be modified in its current status", updateResult.Errors);
    }

    [Theory]
    [InlineData("2024/08/25 12:00", "2024/08/25 16:30")]
    [InlineData("2024/08/25 08:00", "2024/08/25 12:15")]
    public void Update_CancelledEvent_ReturnsFailureResult(string start, string end)      // F8
    {
        // Arrange
        var existingEvent = EventFactory.CreateEventInCancelledStatus();

        // Create StartEventDate 
        var newStartDateResult = StartEventDate.Create(DateTime.Parse(start));
        if (!newStartDateResult.IsSuccess)
        {
            Assert.Fail("Failed to create StartEventDate: " + string.Join(", ", newStartDateResult.Errors));
            return;
        }

        // Create EndEventDate 
        var newEndDateResult = EndEventDate.Create(DateTime.Parse(end));
        if (!newEndDateResult.IsSuccess)
        {
            Assert.Fail("Failed to create EndEventDate: " + string.Join(", ", newEndDateResult.Errors));
            return;
        }

        // Act
        var updateResult = existingEvent.Data.UpdateEventTimes(newStartDateResult.Data, newEndDateResult.Data);

        // Assert
        Assert.False(updateResult.IsSuccess);
        Assert.Contains("Event cannot be modified in its current status", updateResult.Errors);
    }

    [Theory]
    [InlineData("2024/08/30 08:00", "2024/08/30 18:01")] // 10 hours, 1 minute
    [InlineData("2024/08/30 14:59", "2024/08/31 01:00")] // 10 hours, 1 minute
    [InlineData("2024/08/30 14:00", "2024/08/31 00:01")] // 10 hours, 1 minute
    [InlineData("2024/08/30 14:00", "2024/08/31 18:30")] // 28 hours, 30 minutes
    public void Update_DurationExceedsTenHours_ReturnsFailureResult(string start, string end)
    {
        // Arrange
        var existingEvent = EventFactory.CreateEventInDraftStatus();

        // Create StartEventDate 
        var newStartDateResult = StartEventDate.Create(DateTime.Parse(start));
        if (!newStartDateResult.IsSuccess)
        {
            Assert.Fail("Failed to create StartEventDate: " + string.Join(", ", newStartDateResult.Errors));
            return;
        }

        // Create EndEventDate 
        var newEndDateResult = EndEventDate.Create(DateTime.Parse(end));
        if (!newEndDateResult.IsSuccess)
        {
            Assert.Fail("Failed to create EndEventDate: " + string.Join(", ", newEndDateResult.Errors));
            return;
        }

        // Act
        var updateResult = existingEvent.Data.UpdateEventTimes(newStartDateResult.Data, newEndDateResult.Data);

        // Assert
        Assert.False(updateResult.IsSuccess);
        Assert.Contains("Event duration must be between 1 and 10 hours", updateResult.Errors);
    }
}

