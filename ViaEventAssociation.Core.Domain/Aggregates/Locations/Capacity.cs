namespace ViaEventAssociation.Core.Domain.Aggregates.Locations;

public class Capacity
{
    public int capacity { get; }

    public Capacity(int capacity)
    {
        Validate();
        this.capacity = capacity;
    }

    private void Validate()
    {
        
    }
}