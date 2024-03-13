using ViaEventAssociation.Core.Domain.Aggregates.Requests.ValueObjects;

namespace UnitTests.Common.Values.RequestValues;
using Xunit;
public class RequestIdTests
{
    
    [Fact]
    public void Create_GeneratesValidGuid_ReturnsSuccessResult()
    {
        // Act
        var result = RequestId.Create();

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
        var result = RequestId.Create(validGuidString);

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
        var result = RequestId.Create(invalidGuidString);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("Invalid RequestId format.", result.Errors);
    }

    [Fact]
    public void Create_EmptyGuidString_ReturnsFailureResult()
    {
        // Arrange
        string emptyGuidString = "";

        // Act
        var result = RequestId.Create(emptyGuidString);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("Invalid RequestId format.", result.Errors);
    }
}