using ViaEventAssociation.Core.Tools.OperationResult;
using ViaEventAssociation.Core.Domain.Aggregates.Locations;


namespace UnitTests.Features.ViaLocations;

public class LocationFactory
{
    public static Result<Location> CreateAvailableLocation()
    {
        var name = LocationName.Create("testName");
        var cap = LocationCapacity.Create(14);
        var address = Address.Create("Test Address");
        var id = LocationId.Create(1);

        var results =   new Result[] { name, cap, address, id };
       
        if (results.All(r => r.IsSuccess)) 
        {
            return Location.Create(id.Data,address.Data, name.Data,cap.Data);
        } 
        else 
        {
            var errors = results.Where(r => !r.IsSuccess)
                .SelectMany(r => r.Errors)
                .ToArray();
            return Result<Location>.Failure(errors);
        }

    }
}