namespace ViaEventAssociation.Core.Domain.Aggregates.Locations;

public class Location
{
    private LocationName name;
    private Capacity capacity;
    private Adress adress;
    private bool available = true;

    public Location(LocationName name, Capacity capacity, Adress adress)
    {
        this.name = name;
        this.capacity = capacity;
        this.adress = adress;
    }

    public Result setMaximumNumberOfPeople(Capacity capacity)
    {
        this.capacity = capacity;
    }

    public Result setAvailability(bool availability)
    {
        this.available = availability;
    }

}