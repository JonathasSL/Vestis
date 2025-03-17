namespace Vestis._03_Domain.tests.Fakers;

internal class AddressEntityFaker : Faker<AddressEntity>
{
    public AddressEntityFaker()
    {
        RuleFor(a => a.Street, f => f.Address.StreetName());
        RuleFor(a => a.Number, f => f.Address.BuildingNumber());
        RuleFor(a => a.City, f => f.Address.City());
        RuleFor(a => a.State, f => f.Address.State());
        RuleFor(a => a.ZipCode, f => f.Address.ZipCode());
        RuleFor(a => a.Country, f => f.Address.Country());
    }
}
