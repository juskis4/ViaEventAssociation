using ViaEventAssociation.Core.Domain.Aggregates.Invitations.ValueObjects;
using Xunit;

namespace UnitTests.Common.Values.InvitaionValues;

public class InvitationIdTests
{
      
    [Fact]
    public void Create_GeneratesValidGuid_ReturnsSuccessResult()
    {
        // Act
        var result = InvitationId.Create();

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
        var result = InvitationId.Create(validGuidString);

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
        var result = InvitationId.Create(invalidGuidString);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("Invalid InvitationId format.", result.Errors);
    }

    [Fact]
    public void Create_EmptyGuidString_ReturnsFailureResult()
    {
        // Arrange
        string emptyGuidString = "";

        // Act
        var result = InvitationId.Create(emptyGuidString);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("Invalid InvitationId format.", result.Errors);
    }
}