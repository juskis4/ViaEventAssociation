namespace ViaEventAssociation.Core.Domain.Aggregates.Locations;

public class LocationId
{
    public int id { get; }

    public LocationId(int id)
    {
        Validate();
        this.id = id;
    }

    private void Validate()
    {
        
    }
}