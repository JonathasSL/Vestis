using System.ComponentModel.DataAnnotations;
using Vestis.Entities;

namespace Vestis._02_Domain.Entities;

public class ClientEntity : BaseEntity<Guid>
{
    [Required]
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string PhoneNumber { get; private set; }
    public AddressEntity Address { get; private set; }
    public Guid AddressId { get; private set; }
    public StudioEntity Studio { get; private set; }
    public Guid StudioId { get; private set; }

    public ClientEntity(string name, string email, string phoneNumber)
    {
        Name = name;
        Email = email;
        PhoneNumber = phoneNumber;
    }

    //Constructor for EF
    public ClientEntity() { }

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
    public void ChangePhoneNumber(string phoneNumber)
    {
        if (PhoneNumber != phoneNumber)
        {
            PhoneNumber = phoneNumber;
            SetAsUpdated();
        }
    }
    public void ChangeAddress(AddressEntity address)
    {
        if (!Address.Equals(address))
        {
            Address = address;
            SetAsUpdated();
        }
    }
}
