using System.ComponentModel.DataAnnotations;
using Vestis.Entities;

namespace Vestis._02_Domain.Entities;

public class AddressEntity : BaseEntity<Guid>
{
    public string Street { get; private set; }
    public string Number { get; private set; }
    public string Complement { get; private set; }
    public string Neighborhood { get; private set; }
    public string City { get; private set; }
    public string State { get; private set; }
    public string Country { get; private set; } 
    public string ZipCode { get; private set; }

    public AddressEntity(string street, string number, string neighborhood, string city, string state, string country, string zipCode)
    {
        Street = street;
        Number = number;
        Neighborhood = neighborhood;
        City = city;
        State = state;
        Country = country;
        ZipCode = zipCode;
    }

    //Constructor for EF
    public AddressEntity() { }


    public void ChangeStreet(string street)
    {
        if (Street != street)
        {
            Street = street;
            SetAsUpdated();
        }
    }
    public void ChangeNumber(string number)
    {
        if (Number != number)
        {
            Number = number;
            SetAsUpdated();
        }
    }
    public void ChangeComplement(string complement)
    {
        if (Complement != complement)
        {
            Complement = complement;
            SetAsUpdated();
        }
    }
    public void ChangeNeighborhood(string neighborhood)
    {
        if (Neighborhood != neighborhood)
        {
            Neighborhood = neighborhood;
            SetAsUpdated();
        }
    }
    public void ChangeCity(string city)
    {
        if (City != city)
        {
            City = city;
            SetAsUpdated();
        }
    }
    public void ChangeState(string state)
    {
        if (State != state)
        {
            State = state;
            SetAsUpdated();
        }
    }
    public void ChangeCountry(string country)
    {
        if (Country != country)
        {
            Country = country;
            SetAsUpdated();
        }
    }
    public void ChangeZipCode(string zipCode)
    {
        if (ZipCode != zipCode)
        {
            ZipCode = zipCode;
            SetAsUpdated();
        }
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