namespace Vestis._03_Domain.tests.Fakers;

internal class RoleEntityFaker : Faker<RoleEntity>
{
    public RoleEntityFaker()
    {
        RuleFor(r => r.Id, f => Guid.NewGuid());
        RuleFor(r => r.Name, f => f.Lorem.Word());
        RuleFor(r => r.Description, f => f.Lorem.Sentence());
    }
}
