namespace Vestis._03_Domain.Entities;

public class EmailVerificationTokenEntity : BaseEntity<Guid>
{
    #region properties
    public Guid UserId { get; private set; }
    public virtual UserEntity User { get; private set; } = default!;
    public string Token { get; private set; } = default!;
    public DateTime ExpirationDateUtc { get; private set; }
    public bool IsUsed { get; private set; } = false;
    #endregion properties

    #region behavior
    [Obsolete("This constructor is for EF use only.")]
    public EmailVerificationTokenEntity() { }

    public EmailVerificationTokenEntity(Guid userId, string token, DateTime expirationDateUtc)
    {
        UserId = userId;
        Token = token;
        ExpirationDateUtc = expirationDateUtc;
    }

    public bool IsExpired() => DateTime.UtcNow > ExpirationDateUtc;

    public bool Use()
    {
        if (!IsUsed && !IsExpired())
        {
            IsUsed = true;
            SetAsUpdated();
            return true;
        }
        else
            return false;
    }

    #endregion behavior
}
