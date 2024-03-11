using ViaEventAssociation.Core.Tools.OperationResult;

namespace ViaEventAssociation.Core.Domain.Aggregates.Locations;

public class Location
{
    private LocationName _name;
    private Capacity _capacity;
    private Address _address;
    private bool _available = true;

    public Location(LocationName name, Capacity capacity, Address address)
    {
        _name = name;
        _capacity = capacity;
        _address = address;
    }

    public Result SetMaximumNumberOfPeople(Capacity capacity)
    {
        _capacity = capacity;
        return Result.Success();
    }

    public Result UpdateLocationName(LocationName name)
    {
        _name = name;
        return Result.Success();
    }

    public Result SetAvailability(bool availability)
    {
        _available = availability;
        return Result.Success();
    }

}