namespace ViaEventAssociation.Core.Domain.Aggregates.Locations;

public class Adress
{
    public string address { get; }

    public Adress(string address)
    {
        Validate();
        this.address = address;
    }

    private void Validate()
    {
        
    }
}