using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.Locations;

public class Location
{
    public LocationId Id { get; set; }
    public  LocationName Name { get; set; }
    public LocationCapacity Capacity { get; set; }
    public Address Address { get; }
    public bool Available { get; set; } = true;

    private Location(LocationId id, LocationName name, LocationCapacity capacity, Address address)
    {
        Id = id;
        Name = name;
        Capacity = capacity;
        Address = address;
    }

    public static Result<Location> Create(LocationId id, Address address, LocationName name, LocationCapacity capacity)
    {
        return Result<Location>.Success(new Location(id,name, capacity, address));
    }

    public Result SetMaximumNumberOfPeople(int capacity)
    {
        var capResult = LocationCapacity.Create(capacity);
        if (!capResult.IsSuccess)
        {
            return Result.Failure(capResult.Errors.ToArray());
        }

        Capacity = capResult.Data;
        return Result.Success();
    }

    public Result UpdateLocationName(string name)
    {
        var locationNameResult = LocationName.Create(name);
        if (!locationNameResult.IsSuccess)
        {
            return Result.Failure(locationNameResult.Errors.ToArray());
        }

        Name = locationNameResult.Data;
        return Result.Success();
    }

    public Result SetAvailability(bool available)
    {
        Available = available;
        return Result.Success();
    }

}