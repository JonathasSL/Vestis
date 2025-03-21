namespace Vestis._03_Domain.Entities;

public class StudioEntity : BaseEntity<Guid>
{
    #region Properties
    public string Name { get; private set; }
    public string ContactEmail { get; private set; }
    public string PhoneNumber { get; private set; }
    public AddressEntity Address { get; private set; }
    public Guid AddressId { get; set; }
    public virtual List<ClientEntity> Clients { get; private set; }
    #endregion Properties

    #region Behavior
    public StudioEntity(string name)
    {
        Name = name;
    }

    //Constructor for EF
    [Obsolete("This constructor is for EF use only.")]
    public StudioEntity() { }

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

    public void ChangeContactEmail(string contactEmail)
    {
        if (ContactEmail != contactEmail)
        {
            ContactEmail = contactEmail;
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

    public void AddClient(ClientEntity client)
    {
        Clients ??= new List<ClientEntity>();
        Clients.Add(client);
        SetAsUpdated();
    }
    #endregion Behavior
}
