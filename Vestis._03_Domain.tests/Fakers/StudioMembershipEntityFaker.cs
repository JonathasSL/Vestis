namespace Vestis._03_Domain.tests.Fakers;

internal class StudioMembershipEntityFaker : Faker<StudioMembershipEntity>
{
    public StudioMembershipEntityFaker()
    {
        RuleFor(sm => sm.Id, f => Guid.NewGuid());
        RuleFor(sm => sm.User, f => new UserEntityFaker().Generate());
        RuleFor(sm => sm.Role, f => new Faker().Random.Word());
        RuleFor(sm => sm.Studio, f => new StudioEntityFaker().Generate());
    }
}
