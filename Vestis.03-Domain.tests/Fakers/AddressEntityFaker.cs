namespace Vestis._03_Domain.tests.Fakers;

internal class AddressEntityFaker : Faker<AddressEntity>
{
    public AddressEntityFaker()
    {
        var address = new Faker().Address;
        RuleFor(a => a.Street, _ => address.StreetName());
        RuleFor(a => a.Number, _ => address.BuildingNumber());
        RuleFor(a => a.Complement, f => f.Lorem.Word());
        RuleFor(a => a.Neighborhood, f => f.Lorem.Word());
        RuleFor(a => a.City, _ => address.City());
        RuleFor(a => a.State, _ => address.State());
        RuleFor(a => a.ZipCode, _ => address.ZipCode());
        RuleFor(a => a.Country, _ => address.Country());
    }
}
