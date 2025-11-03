namespace Vestis._03_Domain.tests.Tests;

public class BodyMeasurementEntityTests
{
    [Fact]
    public void AddEntry_WithValidData_ShouldAddEntry()
    {
        // Arrange
        var measurement = new MeasurementEntryEntity(
            new Faker().PickRandom(new[] { "Braço", "Cintura", "Peito", "Coxa" }),
            new Faker().Random.Double(max: 200));
        
        var bodyMeasurement = new BodyMeasurementEntityFaker().Generate();

        // Act
        bodyMeasurement.AddEntry(measurement.Name, measurement.Value);

        // Assert
        Assert.Contains(measurement, bodyMeasurement.Entries);
    }

    [Fact]
    public void RemoveEntry_WithValidData_ShouldRemoveEntry()
    {
        // Arrange
        var measurement = new MeasurementEntryEntity(
            new Faker().PickRandom(new[] { "Braço", "Cintura", "Peito", "Coxa" }),
            new Faker().Random.Double(max: 200));

        var bodyMeasurement = new BodyMeasurementEntityFaker().Generate();
        bodyMeasurement.AddEntry(measurement.Name, measurement.Value);
        
        // Act
        bodyMeasurement.RemoveEntry(measurement);
        
        // Assert
        Assert.DoesNotContain(measurement, bodyMeasurement.Entries);
    }
}
