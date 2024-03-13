using ViaEventAssociation.Core.Domain.Aggregates.Requests.ValueObjects;

namespace UnitTests.Common.Values.RequestValues;
using Xunit;
public class AttendanceReasonTests
{
    [Fact]
    public void Create_ValidAttendanceReason_ReturnSuccessResult()
    {
        // Arrange
        const string reason = "interested";
        
        // Act
        var attendance = AttendanceReason.Create(reason);
        
        // Assert
        Assert.True(attendance.IsSuccess);
    }
    
    [Fact]
    public void Create_EmptyAttendanceReason_ReturnFailureResult()
    {
        // Arrange
        const string reason = "";
        
        // Act
        var attendance = AttendanceReason.Create(reason);

        // Assert
        Assert.False(attendance.IsSuccess);
        Assert.Contains("Reason cannot be Empty.", attendance.Errors);
    }
    
    [Fact]
    public void Create_WhitespaceAttendanceReason_ReturnFailureResult()
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
    public void Create_ShortAttendanceReason_ReturnFailureResult()
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
    public void Create_Short3CharAttendanceReason_ReturnSuccessResult()
    {
        // Arrange
        const string reason = "ABC";
        
        // Act
        var attendance = AttendanceReason.Create(reason);

        // Assert
        Assert.True(attendance.IsSuccess);
    }
    
    [Fact]
    public void Create_Long200AttendanceReason_ReturnSuccessResult()
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
    public void Create_LongAttendanceReason_ReturnFailureResult()
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
    
}