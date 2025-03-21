namespace Vestis._03_Domain.tests.Fakers;

internal class UserEntityFaker : Faker<UserEntity>
{
    public UserEntityFaker()
    {
        RuleFor(u => u.Id, f => Guid.NewGuid());
        RuleFor(u => u.Name, f => f.Name.FullName());
        RuleFor(u => u.Email, f => f.Internet.Email());
        RuleFor(u => u.Password, f => f.Internet.Password());
        RuleFor(u => u.CreatedDate, f => f.Date.Past(2));
        RuleFor(u => u.UpdatedDate, f => f.Date.Recent());
    }
}
