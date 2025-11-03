namespace Vestis._03_Domain.tests.Fakers;

internal class StudioEntityFaker : Faker<StudioEntity>
{
    public StudioEntityFaker()
    {
        RuleFor(s => s.Id, f => Guid.NewGuid());
        RuleFor(s => s.Name, f => f.Company.CompanyName());
        RuleFor(s => s.ContactEmail, f => f.Internet.Email());
        RuleFor(s => s.PhoneNumber, f => f.Phone.PhoneNumber());
        RuleFor(s => s.Address, _ => new AddressEntityFaker().Generate());
    }

    public StudioEntityFaker WithClients(int clientsCount)
    {
        //RuleFor(s => s.Clients, f => new ClientEntityFaker().Generate(clientsCount));
        return this;
    }

    public StudioEntityFaker WithName(string name)
    {
        RuleFor(s => s.Name, _ => name);
        return this;
    }
}
