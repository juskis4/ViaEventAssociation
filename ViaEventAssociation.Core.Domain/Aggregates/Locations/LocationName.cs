namespace ViaEventAssociation.Core.Domain.Aggregates.Locations;

public class LocationName
{
    public string name { get; }
    public LocationName(string name)
    {
        Validate();
        this.name = name;
    }

    private void Validate()
    {
        
    }
}