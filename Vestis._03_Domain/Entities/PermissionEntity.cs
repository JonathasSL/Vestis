namespace Vestis._03_Domain.Entities;

public class PermissionEntity : BaseEntity<Guid>
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public Guid RoleId { get; private set; }
    public RoleEntity Role { get; private set; }
    public PermissionEntity(string name, string description)
    {
        Name = name;
        Description = description;
    }

    //Constructor for EF
    [Obsolete("This constructor is for EF use only.")]
    public PermissionEntity() { }
}
