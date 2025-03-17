namespace Vestis._03_Domain.tests.Fakers;

internal class MeasurementEntryEntityFaker : Faker<MeasurementEntryEntity>
{
    public MeasurementEntryEntityFaker()
    {
        RuleFor(m => m.Id, f => Guid.NewGuid());
        RuleFor(m => m.Name, f => f.PickRandom(new[] { "Braço", "Cintura", "Peito", "Coxa" }));
        RuleFor(m => m.Value, f => f.Random.Decimal(30, 150));
        //RuleFor(m => m.Unit, _ => "cm");
    }
}
