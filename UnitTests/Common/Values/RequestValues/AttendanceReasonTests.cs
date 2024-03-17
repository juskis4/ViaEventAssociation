using ViaEventAssociation.Core.Domain.Aggregates.Requests.ValueObjects;

namespace UnitTests.Common.Values.RequestValues;
using Xunit;
public class AttendanceReasonTests
{
    [Fact]
    public void Create_ValidAttendanceReason_ReturnsSuccessResult()
    {
        // Arrange
        const string reason = "interested";
        
        // Act
        var attendance = AttendanceReason.Create(reason);
        
        // Assert
        Assert.True(attendance.IsSuccess);
    }
    
    [Fact]
    public void Create_EmptyAttendanceReason_ReturnsFailureResult()
    {
        // Arrange
        const string reason = "";
        
        // Act
        var attendance = AttendanceReason.Create(reason);

        // Assert
        Assert.False(attendance.IsSuccess);
        Assert.Contains("Reason cannot be null or Empty.", attendance.Errors);
    }
    
    [Fact]
    public void Create_WhitespaceAttendanceReason_ReturnsFailureResult()
    {
        // Arrange
        const string reason = " ";
        
        // Act
        var attendance = AttendanceReason.Create(reason);

        // Assert
        Assert.False(attendance.IsSuccess);
        Assert.Contains("Reason cannot be Whitespace.", attendance.Errors);
    }
    
    [Fact]
    public void Create_ShortAttendanceReason_ReturnsFailureResult()
    {
        // Arrange
        const string reason = "AB";
        
        // Act
        var attendance = AttendanceReason.Create(reason);

        // Assert
        Assert.False(attendance.IsSuccess);
        Assert.Contains("AttendanceReason cannot be less than 3 Characters.", attendance.Errors);
    }
    
    [Fact]
    public void Create_Short3CharAttendanceReason_ReturnsSuccessResult()
    {
        // Arrange
        const string reason = "ABC";
        
        // Act
        var attendance = AttendanceReason.Create(reason);

        // Assert
        Assert.True(attendance.IsSuccess);
    }
    
    [Fact]
    public void Create_Long200AttendanceReason_ReturnsSuccessResult()
    {
        // Arrange
        const string reason = "IgSE12JoRXigVzEmXDha5baGrP0yMJrtmiTRhcKOVEXHVW2lKL13p6EkL998SmVSz9Wa3ihn7SlmdVLrxnHYruKw0uUjkTDOYUjmRqZxYJsI6PR93JMIyp0EP25Ma2HUvStvbH8Lalim2Jbid7e3L7V2axbVJHmItxyF6g1A4OoXqDyUpe4LH3Hd8i8fFBUlYcYAly8j";
        
        // Act
        var attendance = AttendanceReason.Create(reason);

        // Assert
        Assert.True(attendance.IsSuccess);
        Assert.Equal(200, reason.Length);
    }
    
    [Fact]
    public void Create_LongAttendanceReason_ReturnsFailureResult()
    {
        // Arrange
        const string reason = "IgSE12JoRXigVzEmXDha5baGrP0yMJrtmiTRhcKOVEXHVW2lKL13p6EkL998SmVSz9Wa3ihn7SlmdVLrxnHYruKw0uUjkTDOYUjmRqZxYJsI6PR93JMIyp0EP25Ma2HUvStvbH8Lalim2Jbid7e3L7V2axbVJHmItxyF6g1A4OoXqDyUpe4LH3Hd8i8fFBUlYcYAly8ja";
        
        // Act
        var attendance = AttendanceReason.Create(reason);

        // Assert
        Assert.False(attendance.IsSuccess);
        Assert.Contains("AttendanceReason is too Long.", attendance.Errors);
        Assert.Equal(201, reason.Length);
    }
    
    [Fact]
    public void Create_NullAttendanceReason_ReturnsFailureResult()
    {
        // Arrange
        const string reason = null;
        
        // Act
        var attendance = AttendanceReason.Create(reason);

        // Assert
        Assert.False(attendance.IsSuccess);
        Assert.Contains("Reason cannot be null or Empty.", attendance.Errors);
    }
    
}