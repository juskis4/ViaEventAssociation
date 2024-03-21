using ViaEventAssociation.Core.Domain.Aggregates.Events;
using ViaEventAssociation.Core.Domain.Aggregates.Events.ValueObjects;
using Xunit;

namespace ViaEventAssociation.Core.Tests.Domain.Aggregates.Events;

public class EventDescriptionTests
{
    // Success Scenarios

    [Fact]
    public void Create_ValidDescription_ReturnsSuccessResult()
    {
        // Arrange
        string descriptionText = "This is a cool event you should attend!"; 

        // Act
        var result = Description.Create(descriptionText);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(descriptionText, result.Data.DescriptionText);
    }

    [Fact]
    public void Create_DescriptionWithMaxLength_ReturnsSuccessResult()
    {
        // Arrange
        string descriptionText = new string('X', 250);

        // Act
        var result = Description.Create(descriptionText);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(descriptionText, result.Data.DescriptionText);
    }

    [Fact]
    public void Create_NullDescription_ReturnsSuccessResult_WithDefaultValue() 
    {
        // Arrange
        string descriptionText = null;

        // Act
        var result = Description.Create(descriptionText);

        // Assert
        Assert.True(result.IsSuccess); 
        Assert.Equal("", result.Data.DescriptionText); 
    }

    [Fact]
    public void Create_EmptyDescription_ReturnsSuccessResult_WithDefaultValue()  
    {
        // Arrange
        string descriptionText = "";

        // Act
        var result = Description.Create(descriptionText);

        // Assert
        Assert.True(result.IsSuccess); 
        Assert.Equal("", result.Data.DescriptionText); 
    }

    // Failure Scenarios 

    [Fact]
    public void Create_DescriptionExceedingMaxLength_ReturnsFailureResult()
    {
        // Arrange
        string descriptionText = new string('X', 251); 

        // Act
        var result = Description.Create(descriptionText);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("Description cannot exceed 250 characters.", result.Errors);
    }
}
