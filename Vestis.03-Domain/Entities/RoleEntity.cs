namespace Vestis._03_Domain.Entities;

public class RoleEntity : BaseEntity<Guid>
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public HashSet<PermissionEntity> Permissions { get; private set; }

    public RoleEntity(string name, string description, StudioEntity studio)
    {
        Name = name;
        Description = description;
    }

    //Constructor for EF
    [Obsolete("This constructor is for EF use only.")]
    public RoleEntity() { }

    public bool AddPermission(PermissionEntity permission)
    {
        if (Permissions == null)
            Permissions = new HashSet<PermissionEntity>();
        return Permissions.Add(permission);
    }
}
