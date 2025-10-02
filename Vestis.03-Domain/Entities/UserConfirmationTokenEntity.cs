namespace Vestis._03_Domain.Entities;

public class UserConfirmationTokenEntity : BaseEntity<Guid>
{
    public Guid UserId { get; private set; }
    public UserEntity User { get; private set; }

    public Guid Token { get; private set; }
    public DateTime ExpirationDate { get; private set; }
    public bool IsUsed { get; private set; }


    public UserConfirmationTokenEntity(UserEntity user, DateTime expirationDate)
    {
        User = user ?? throw new ArgumentNullException(nameof(user), "User cannot be null.");
        UserId = user.Id;
        Token = Guid.NewGuid();
        ExpirationDate = expirationDate;
        IsUsed = false;
    }

    [Obsolete("This constructor is for EF use only.")]
    public UserConfirmationTokenEntity() { }


    public void Confirm()
    {
        if (!IsUsed)
        {
            IsUsed = true;
            SetAsUpdated();
        }
    }
}
