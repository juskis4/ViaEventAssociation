using ViaEventAssociation.Core.Domain.Aggregates.Events;

namespace UnitTests.Features.Event.UpdateTitle;
using Xunit;

public class UpdateEventTitleTests
{

    [Fact]
    public void UpdateTitle_ValidNewTitleAndReady_ReturnSuccessAndEventUpdated()
    {
        //Arrange
        var eventId = EventId.Create();
        var viaEvent = ViaEvent.Create(eventId.Data);
        viaEvent.Data.Status = Status.Ready;
        // Act
        var result =ViaEvent.UpdateEventTitle(viaEvent.Data, "Scary Movie Night!");
        // Assert 
        Assert.True(result.IsSuccess);
        Assert.Equal("Scary Movie Night!",viaEvent.Data.Title.Title);
    }
    
    [Fact]
    public void UpdateTitle_Title0_ReturnFailure()
    {
        //Arrange
        var eventId = EventId.Create();
        var viaEvent = ViaEvent.Create(eventId.Data);
        viaEvent.Data.Status = Status.Ready;
        // Act
        var result =ViaEvent.UpdateEventTitle(viaEvent.Data, "");
        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains("Title cannot be NULL or Empty.",result.Errors);
    }
    
    [Fact]
    public void UpdateTitle_TitleLessThan3_ReturnFailure()
    {
        //Arrange
        var eventId = EventId.Create();
        var viaEvent = ViaEvent.Create(eventId.Data);
        // Act
        var result =ViaEvent.UpdateEventTitle(viaEvent.Data, "XY");
        // Assert 
        Assert.False(result.IsSuccess);
        Assert.Contains("Title cannot be less than 3 characters.",result.Errors);
    }
    
    
    [Fact]
    public void UpdateTitle_TitleMoreThan75_ReturnFailure()
    {
        //Arrange
        var thisIs76CharString = "CTaRj3Q87jFi1S5wmSKftvCXFtp04iCikDtrN3pWXjB2ArcekQZ9i0yNqvZnEhcfFuktxR5yZMQL";
        var eventId = EventId.Create();
        var viaEvent = ViaEvent.Create(eventId.Data);
        // Act
        var result =ViaEvent.UpdateEventTitle(viaEvent.Data, thisIs76CharString);
        // Assert 
        Assert.False(result.IsSuccess);
        Assert.Contains("Title cannot be more than 75 characters.",result.Errors);
    }
    
    [Fact]
    public void UpdateTitle_TitleNull_ReturnFailure()
    {
        //Arrange
        string nullString = null;
        var eventId = EventId.Create();
        var viaEvent = ViaEvent.Create(eventId.Data);
        // Act
        var result =ViaEvent.UpdateEventTitle(viaEvent.Data, nullString);
        // Assert 
        Assert.False(result.IsSuccess);
        Assert.Contains("Title cannot be NULL or Empty.",result.Errors);
    }
    
    [Fact]
    public void UpdateTitle_ValidTitleAndEventIsActive_ReturnFailure()
    {
        //Arrange
        
        var eventId = EventId.Create();
        var viaEvent = ViaEvent.Create(eventId.Data);
        viaEvent.Data.Status = Status.Active;
        // Act
        var result =ViaEvent.UpdateEventTitle(viaEvent.Data, "Scary Movie Night!");
        // Assert 
        Assert.False(result.IsSuccess);
        Assert.Contains("an Active event cannot be modified",result.Errors);
    }
    
    [Fact]
    public void UpdateTitle_ValidTitleAndEventIsCancelled_ReturnFailure()
    {
        //Arrange
        
        var eventId = EventId.Create();
        var viaEvent = ViaEvent.Create(eventId.Data);
        viaEvent.Data.Status = Status.Cancelled;
        // Act
        var result =ViaEvent.UpdateEventTitle(viaEvent.Data, "Scary Movie Night!");
        // Assert 
        Assert.False(result.IsSuccess);
        Assert.Contains("an Cancelled event cannot be modified",result.Errors);
    }
}