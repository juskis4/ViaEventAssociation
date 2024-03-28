using ViaEventAssociation.Core.Domain.Aggregates.Events;
using ViaEventAssociation.Core.Domain.Aggregates.Events.ValueObjects;
using Xunit;

namespace ViaEventAssociation.Core.Tests.Domain.Aggregates.Events;

public class EventNameTests
{
    // Success Scenarios

    [Theory]
    [InlineData("Valid Event Name")]
    [InlineData("Another Cool Event")]
    [InlineData("Meetup 2023")] 
    public void Create_ValidTitle_ReturnsSuccessResult(string title)
    {
        // Act
        var result = EventName.Create(title);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(title, result.Data.Title);
    }


    // Failure Scenarios 

    [Fact]
    public void Create_NullTitle_ReturnsFailureResult()
    {
        // Arrange
        string title = null;

        // Act
        var result = EventName.Create(title);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("Title cannot be NULL or Empty.", result.Errors);
    }

    [Fact]
    public void Create_EmptyTitle_ReturnsFailureResult()
    {
        // Arrange
        string title = ""; 

        // Act
        var result = EventName.Create(title);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("Title cannot be NULL or Empty.", result.Errors);
    }

    [Fact]
    public void Create_WhitespaceTitle_ReturnsFailureResult()
    {
        // Arrange
        string title = "   ";  

        // Act
        var result = EventName.Create(title);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("Title cannot bee a white space.", result.Errors);
    }

    [Fact]
    public void Create_TitleLessThan3Chars_ReturnsFailureResult()
    {
        // Arrange
        string title = "AB";

        // Act
        var result = EventName.Create(title);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("Title cannot be less than 3 characters.", result.Errors);
    }

    [Fact]
    public void Create_TitleMoreThan75Chars_ReturnsFailureResult()
    {
        // Arrange
        string title = new string('X', 76);

        // Act
        var result = EventName.Create(title);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("Title cannot be more than 75 characters.", result.Errors);
    }
}
