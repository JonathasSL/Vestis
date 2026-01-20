namespace Vestis._03_Domain.Entities;

public class StudioMembershipEntity : BaseEntity<Guid>
{
    public Guid UserId {  get; private set; }
    public virtual UserEntity User { get; private set; }
    /*
    public Guid? ClientId { get; private set; }
    public ClientEntity? Client { get; private set; }
    */
    public string? Role { get; private set; }
    /*
    public Guid RoleId { get; private set; }
    public RoleEntity Role { get; private set; }
    */
    public Guid StudioId { get; private set; }
    public virtual StudioEntity Studio { get; private set; }

    public StudioMembershipEntity(UserEntity user, string roleName, StudioEntity studio)
    {
        UserId = user.Id;
        User = user;

        Role = roleName;
        /*
        RoleId = role.Id;
        Role = role;
        */
        StudioId = studio.Id;
        Studio = studio;
    }

    //Constructor for EF
    [Obsolete("This constructor is for EF use only.")]
    public StudioMembershipEntity() { }

    public void ChangeRole(string role)
    {
        if (Role != role)
        {
            Role = role;
            SetAsUpdated();
        }
    }
}
