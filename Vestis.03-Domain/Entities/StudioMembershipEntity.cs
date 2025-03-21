namespace Vestis._03_Domain.Entities;

public class StudioMembershipEntity : BaseEntity<Guid>
{
    public UserEntity User { get; private set; }
    public ClientEntity Client { get; private set; }
    public RoleEntity Role { get; private set; }
    public StudioEntity Studio { get; private set; }

    public StudioMembershipEntity(UserEntity user, RoleEntity role, StudioEntity studio)
    {
        User = user;
        Role = role;
        Studio = studio;
    }

    //Constructor for EF
    [Obsolete("This constructor is for EF use only.")]
    public StudioMembershipEntity() { }

    public void ChangeRole(RoleEntity role)
    {
        if (Role != role)
        {
            Role = role;
            SetAsUpdated();
        }
    }
}
