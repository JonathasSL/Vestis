using Vestis.Shared.Extensions;

namespace Vestis._03_Domain.Entities;

public class ProjectEntity : BaseEntity<Guid>
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public Guid StudioId { get; private set; }
    public StudioEntity Studio { get; private set; }
    public Guid ClientId { get; private set; }
    public ClientEntity Client { get; private set; }
    public BodyMeasurementEntity BodyMeasurements { get; private set; }
    
    public ProjectEntity(StudioEntity studioEntity, string name)
    {
        Studio = studioEntity;
        StudioId = studioEntity.Id;
        Name = name.EmptyToNull() ?? throw new ArgumentException("Name cannot be null or empty.");
    }

    //Constructor for EF
    [Obsolete("This constructor is for EF use only.")]
    public ProjectEntity() { }

    public void ChangeName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be null or empty.");
        else if (Name != name)
        {
            Name = name;
            SetAsUpdated();
        }
    }
    public void ChangeDescription(string description)
    {
        if (Description != description)
        {
            Description = description;
            SetAsUpdated();
        }
    }
    public void ChangeClient(ClientEntity client)
    {
        if (!Client.Equals(client))
        {
            Client = client;
            ClientId = client.Id;
            SetAsUpdated();
        }
    }
}
