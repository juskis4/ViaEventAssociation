using ViaEventAssociation.Core.Domain.Common.Bases;
using Xunit;

namespace UnitTests.Common.Values.GuestsValues;
//TODO: This is a setup class where should be implement more tests when implementing Validate GuestId method.
public class GuestIdTests
{     [Fact]
    public void Create_ValidName_ReturnsSuccessResult()
    {
        // Arrange
        var result = GuestId.Create(123);
            
        Assert.True(result.IsSuccess);
        Assert.Equal(123,result.Data.id);
    }

}