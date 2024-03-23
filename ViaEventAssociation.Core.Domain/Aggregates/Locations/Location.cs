using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.Locations;

public class Location
{
    public  LocationName name { get; set; }
    public LocationCapacity capacity { get; set; }
    public Address address { get; }
    public bool available { get; set; } = true;

    private Location(LocationName name, LocationCapacity capacity, Address address)
    { 
        this.name = name;
        this.capacity = capacity;
        this.address = address;
    }

    public static Result<Location> Create(LocationId id, Address address, LocationName name, LocationCapacity capacity)
    {
        return Result<Location>.Success(new Location(name, capacity, address));
    }

    public Result SetMaximumNumberOfPeople(int capacity)
    {
        var capResult = LocationCapacity.Create(capacity);
        if (!capResult.IsSuccess)
        {
            return Result.Failure(capResult.Errors.ToArray());
        }

        this.capacity = capResult.Data;
        return Result.Success();
    }

    public Result UpdateLocationName(string name)
    {
        var locationNameResult = LocationName.Create(name);
        if (!locationNameResult.IsSuccess)
        {
            return Result.Failure(locationNameResult.Errors.ToArray());
        }

        this.name = locationNameResult.Data;
        return Result.Success();
    }

    public Result SetAvailability(bool availability)
    {
        this.available = availability;
        return Result.Success();
    }

}