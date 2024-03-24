using Xunit;

namespace UnitTests.Features.ViaLocations.UpdateLocationName;

public class UpdateLocationNameTests
{
    [Theory]
    [InlineData("firstUpdate")]
    [InlineData("secondUpdate")]
    [InlineData("FinalUpdate")]
    public void LocationName_ValidArguments_ResulSuccess(string updateName)
    {
        var newLocationResult =  LocationFactory.CreateAvailableLocation();
        if (!newLocationResult.IsSuccess)
        {
            Assert.Fail("Location creation failed: " + string.Join(", ", newLocationResult.Errors)); 
            return;
        }

        newLocationResult.Data.UpdateLocationName(updateName);
        Assert.Equal(updateName,newLocationResult.Data.name.Name);
    }
}