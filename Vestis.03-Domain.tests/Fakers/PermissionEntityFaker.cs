namespace Vestis._03_Domain.tests.Fakers;

internal class PermissionEntityFaker : Faker<PermissionEntity>
{
    public PermissionEntityFaker()
    {
        RuleFor(p => p.Id, f => Guid.NewGuid());
        RuleFor(p => p.Name, f => f.Lorem.Word());
        RuleFor(p => p.Description, f => f.Lorem.Sentence());
    }
}
