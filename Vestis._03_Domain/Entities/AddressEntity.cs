namespace Vestis._03_Domain.Entities;

public class AddressEntity : BaseEntity<Guid>
{
    public string Street { get; private set; }
    public string Number { get; private set; }
    public string? Complement { get; private set; }
    public string Neighborhood { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public string? Country { get; private set; } 
    public string ZipCode { get; private set; }

    public AddressEntity(string street, string number, string neighborhood, string city, string state, string zipCode, string? complement = null, string? country = null)
    {
        Street = street;
        Number = number;
        Neighborhood = neighborhood;
        City = city;
        State = state;
        ZipCode = zipCode;
        Complement = complement;
        Country = country;
    }

    //Constructor for EF
    [Obsolete("This constructor is for EF use only.")]
    public AddressEntity() { }

    public AddressEntity ChangeStreet(string street)
    {
        if (Street != street)
        {
            Street = street;
            SetAsUpdated();
        }
        return this;
    }
    public AddressEntity ChangeNumber(string number)
    {
        if (Number != number)
        {
            Number = number;
            SetAsUpdated();
        }
        return this;
    }
    public AddressEntity ChangeComplement(string complement)
    {
        if (Complement != complement)
        {
            Complement = complement;
            SetAsUpdated();
        }
        return this;
    }
    public AddressEntity ChangeNeighborhood(string neighborhood)
    {
        if (Neighborhood != neighborhood)
        {
            Neighborhood = neighborhood;
            SetAsUpdated();
        }
        return this;
    }
    public AddressEntity ChangeCity(string city)
    {
        if (City != city)
        {
            City = city;
            SetAsUpdated();
        }
        return this;
    }
    public AddressEntity ChangeState(string state)
    {
        if (State != state)
        {
            State = state;
            SetAsUpdated();
        }
        return this;
    }
    public AddressEntity ChangeCountry(string country)
    {
        if (Country != country)
        {
            Country = country;
            SetAsUpdated();
        }
        return this;
    }
    public AddressEntity ChangeZipCode(string zipCode)
    {
        if (ZipCode != zipCode)
        {
            ZipCode = zipCode;
            SetAsUpdated();
        }
        return this;
    }

    public override bool Equals(object? obj)
    {
        return obj is AddressEntity entity &&
               DeletedDate == entity.DeletedDate &&
               Street == entity.Street &&
               Number == entity.Number &&
               Complement == entity.Complement &&
               Neighborhood == entity.Neighborhood &&
               City == entity.City &&
               State == entity.State &&
               Country == entity.Country &&
               ZipCode == entity.ZipCode;
    }
}