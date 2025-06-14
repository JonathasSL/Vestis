namespace Vestis._03_Domain.Entities;

public class StudioMembershipEntity : BaseEntity<Guid>
{
    public Guid UserId {  get; private set; }
    public UserEntity User { get; private set; }
    public Guid ClientId { get; private set; }
    public ClientEntity Client { get; private set; }
    public Guid RoleId { get; private set; }
    public RoleEntity Role { get; private set; }
    public Guid StudioId { get; private set; }
    public StudioEntity Studio { get; private set; }

    public StudioMembershipEntity(UserEntity user, RoleEntity role, StudioEntity studio)
    {
        UserId = user.Id;
        User = user;

        RoleId = role.Id;
        Role = role;

        StudioId = studio.Id;
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
