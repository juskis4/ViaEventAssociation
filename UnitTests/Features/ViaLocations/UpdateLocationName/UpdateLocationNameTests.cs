using Xunit;

namespace UnitTests.Features.ViaLocations.UpdateLocationName;

public class UpdateLocationNameTests
{
    [Theory]
    [InlineData("Firstupdate")]
    [InlineData("Secondupdate")]
    [InlineData("Finalupdate")]
    public void LocationName_ValidArguments_ResulSuccess(string updateName)
    {
        var newLocationResult =  LocationFactory.CreateAvailableLocation();
        if (!newLocationResult.IsSuccess)
        {
            Assert.Fail("Location creation failed: " + string.Join(", ", newLocationResult.Errors)); 
            return;
        }

        newLocationResult.Data.UpdateLocationName(updateName);
        Assert.Equal(updateName,newLocationResult.Data.Name.Name);
    }
    
    [Fact]
    public void LocationName_InvalidArguments_ResulSuccess()
    {
        var newLocationResult =  LocationFactory.CreateAvailableLocation();
        if (!newLocationResult.IsSuccess)
        {
            Assert.Fail("Location creation failed: " + string.Join(", ", newLocationResult.Errors)); 
            return;
        }

        var oldName = newLocationResult.Data.Name.Name;
        var result =  newLocationResult.Data.UpdateLocationName("");
        Assert.False(result.IsSuccess);
        Assert.Equal(oldName,newLocationResult.Data.Name.Name);
    }
}