namespace Vestis._03_Domain.tests.Fakers;

internal class ClientEntityFaker : Faker<ClientEntity>
{
    public ClientEntityFaker()
    {
        RuleFor(c => c.Id, f => Guid.NewGuid());
        RuleFor(c => c.Name, f => f.Company.CompanyName());
        RuleFor(c => c.Email, f => f.Internet.Email());
        RuleFor(c => c.PhoneNumber, f => f.Phone.PhoneNumber());
        RuleFor(c => c.Address, _ => new AddressEntityFaker().Generate());
    }
}
