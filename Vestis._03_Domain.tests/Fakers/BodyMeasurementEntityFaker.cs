namespace Vestis._03_Domain.tests.Fakers;

internal class BodyMeasurementEntityFaker : Faker<BodyMeasurementEntity>
{
    public BodyMeasurementEntityFaker()
    {
        RuleFor(g => g.Id, f => Guid.NewGuid());
        RuleFor(g => g.MeasurementDate, f => f.Date.Past(1));
        RuleFor(g => g.Entries, _ => new MeasurementEntryEntityFaker().Generate(5));
    }
}
