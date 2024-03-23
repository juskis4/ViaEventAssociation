using ViaEventAssociation.Core.Domain.Aggregates.Events;

namespace UnitTests.Features.Event.MakePublic;
using Xunit;

public class MakeEventPublicTests
{

    [Fact]
    public void MakeEventPublic_StatusIsDraft_ReturnsSuccess()
    {
        // Arrange
        var id = EventId.Create(Guid.NewGuid().ToString());
        var result = ViaEvent.Create(id.Data);
        var viaEvent = result.Data;
        // Act
        var newResult = viaEvent.MakeEventPublic();
        // Assert 
        Assert.True(newResult.IsSuccess);
        Assert.True(viaEvent.IsPublic);
        Assert.Equal(Status.Draft, viaEvent.Status);
    }
    
    [Fact]
    public void MakeEventPublic_StatusIsReady_ReturnsSuccess()
    {
        // Arrange
        var id = EventId.Create(Guid.NewGuid().ToString());
        var result = ViaEvent.Create(id.Data);
        var viaEvent = result.Data;
        viaEvent.Status = Status.Ready;
        // Act
        var newResult = viaEvent.MakeEventPublic();
        // Assert 
        Assert.True(newResult.IsSuccess);
        Assert.True(viaEvent.IsPublic);
        Assert.Equal(Status.Ready, viaEvent.Status);
    }
    
    [Fact]
    public void MakeEventPublic_StatusIsActive_ReturnsSuccess()
    {
        // Arrange
        var id = EventId.Create(Guid.NewGuid().ToString());
        var result = ViaEvent.Create(id.Data);
        var viaEvent = result.Data;
        viaEvent.Status = Status.Active;
        // Act
        var newResult = viaEvent.MakeEventPublic();
        // Assert 
        Assert.True(newResult.IsSuccess);
        Assert.True(viaEvent.IsPublic);
        Assert.Equal(Status.Active, viaEvent.Status);
    }
    
    [Fact]
    public void MakeEventPublic_StatusIsCancelled_ReturnsSuccess()
    {
        // Arrange
        var id = EventId.Create(Guid.NewGuid().ToString());
        var viaEvent = ViaEvent.Create(id.Data);

        viaEvent.Data.Status = Status.Cancelled;
        // Act
        var result = viaEvent.Data.MakeEventPublic();
        // Assert 
        Assert.False(result.IsSuccess);
        Assert.False(viaEvent.Data.IsPublic);
        Assert.Equal(Status.Cancelled, viaEvent.Data.Status);
        Assert.Contains("a cancelled event cannot be modified",result.Errors);
    }
    
}