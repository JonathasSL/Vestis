using System.ComponentModel.DataAnnotations;

namespace Vestis._03_Domain.Entities;

public class UserEntity : BaseEntity<Guid>
{
    #region properties
    public string Name { get; private set; }
    [EmailAddress]
    public string Email { get; private set; }
    public string Password { get; private set; }
    public string? ProfileImg { get; private set; }

    public ICollection<StudioMembershipEntity> StudioMemberships { get; private set; } = new List<StudioMembershipEntity>();
    #endregion properties
    
    #region behavior
    public UserEntity(string name, string email, string password, string profileImg = null)
    {
        Name = name;
        Email = email;
        Password = password;
        ProfileImg = profileImg;
    }

    //Constructor for EF
    [Obsolete("This constructor is for EF use only.")]
    public UserEntity() { }

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
    public void ChangeEmail(string email)
    {
        if (Email != email)
        {
            Email = email;
            SetAsUpdated();
        }
    }
    public void ChangePassword(string password)
    {
        if (Password != password)
        {
            Password = password;
            SetAsUpdated();
        }
    }
    public void ChangeProfileImg(string? profileImg)
    {
        if (ProfileImg != profileImg)
        {
            ProfileImg = profileImg;
            SetAsUpdated();
        }
    }
    #endregion behavior
}
