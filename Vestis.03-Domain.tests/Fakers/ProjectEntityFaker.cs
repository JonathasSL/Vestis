namespace Vestis._03_Domain.tests.Fakers;

internal class ProjectEntityFaker : Faker<ProjectEntity>
{
    public ProjectEntityFaker()
    {
        RuleFor(p => p.Id, f => Guid.NewGuid());
        RuleFor(p => p.Description, f => f.Lorem.Sentence());
        RuleFor(p => p.CreatedDate, f => f.Date.Past(2));
        RuleFor(p => p.Client, _ => new ClientEntityFaker().Generate());
        RuleFor(p => p.BodyMeasurements, _ => new BodyMeasurementEntityFaker().Generate(3));
        //RuleFor(p => p.DeletedDate, f => f.Date.Future(1));
    }
}
