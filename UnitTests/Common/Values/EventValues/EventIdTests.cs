using ViaEventAssociation.Core.Domain.Aggregates.Events;
using Xunit;

namespace ViaEventAssociation.Core.Tests.Domain.Aggregates.Events;

public class EventIdTests
{
    // Success Scenarios 

    [Fact]
    public void Create_GeneratesValidGuid_ReturnsSuccessResult()
    {
        // Act
        var result = EventId.Create();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotEqual(Guid.Empty, result.Data.Id);
    }

    [Fact]
    public void Create_ValidGuidString_ReturnsSuccessResult()
    {
        // Arrange
        var validGuidString = Guid.NewGuid().ToString();

        // Act
        var result = EventId.Create(validGuidString);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(Guid.Parse(validGuidString), result.Data.Id);
    }

    // Failure Scenarios 

    [Fact]
    public void Create_InvalidGuidString_ReturnsFailureResult()
    {
        // Arrange
        string invalidGuidString = "not-a-valid-guid";

        // Act
        var result = EventId.Create(invalidGuidString);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("Invalid EventId format.", result.Errors);
    }

    [Fact]
    public void Create_EmptyGuidString_ReturnsFailureResult()
    {
        // Arrange
        string emptyGuidString = "";

        // Act
        var result = EventId.Create(emptyGuidString);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("Invalid EventId format.", result.Errors);
    }
}
