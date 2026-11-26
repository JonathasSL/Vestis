using Vestis.Shared.Extensions;

namespace Vestis._03_Domain.Entities;

public class ClientEntity : BaseEntity<Guid>
{
    public string Name { get; private set; }
    public string? Email { get; private set; }
    public string? PhoneNumber { get; private set; }
    public virtual AddressEntity? Address { get; private set; }
    public Guid? AddressId { get; private set; }
    public virtual StudioEntity Studio { get; private set; }
    public Guid StudioId { get; private set; }

    public ClientEntity(string name, Guid studioId)
    {
        Name = name.Trim().EmptyToNull() ?? throw new ArgumentNullException(nameof(name));
        StudioId = studioId;
    }

    //Constructor for EF
    [Obsolete("This constructor is for EF use only.")]
    public ClientEntity() { }

    public ClientEntity ChangeName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be null or empty.");
        else if (Name != name)
        {
            Name = name;
            SetAsUpdated();
        }
        return this;
    }
    public ClientEntity ChangeEmail(string email)
    {
        if (Email != email)
        {
            Email = email;
            SetAsUpdated();
        }
        return this;
    }
    public ClientEntity ChangePhoneNumber(string phoneNumber)
    {
        if (PhoneNumber != phoneNumber)
        {
            PhoneNumber = phoneNumber;
            SetAsUpdated();
        }
        return this;
    }
    public ClientEntity ChangeAddress(AddressEntity address)
    {
        if (Address is null && address is not null)
        {
            Address = address;
            AddressId = address.Id;
            SetAsUpdated();
        }
        else if (!Address.Equals(address))
        {
            Address = address;
            SetAsUpdated();
        }
        return this;
    }
}
