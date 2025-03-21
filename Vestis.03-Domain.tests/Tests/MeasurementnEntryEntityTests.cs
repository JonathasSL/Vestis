namespace Vestis._03_Domain.tests.Tests;

public class MeasurementnEntryEntityTests
{
    [Fact]
    public void WhenCreated_ShouldHaveName()
    {
        var faker = new Faker();

        // Arrange & Act
        var measurement = new MeasurementEntryEntity(faker.Lorem.Word(), faker.Random.Double(max: 200));
        
        // Assert
        Assert.NotNull(measurement.Name);
    }

    [Fact]
    public void WhenCreated_ShouldHaveValue()
    {
        var faker = new Faker();

        // Arrange & Act
        var measurement = new MeasurementEntryEntity(faker.Lorem.Word(), faker.Random.Double(max: 200));
        // Act
        // Assert
        Assert.NotEqual(0, measurement.Value);
    }

    [Fact]
    public void WhenChangingName_ShouldHaveName()
    {
        //Arrange
        var newName = new Faker().Person.FullName;
        var measurement = new MeasurementEntryEntityFaker().Generate();

        //Act
        measurement.ChangeName(newName);

        //Assert
        Assert.Equal(newName, measurement.Name);
    }

    [Fact]
    public void WhenChangingValue_ShouldHaveValue()
    {
        //Arrange
        var newValue = new Faker().Random.Double(max: 200);
        var measurement = new MeasurementEntryEntityFaker().Generate();

        //Act
        measurement.ChangeValue(newValue);

        //Assert
        Assert.Equal(newValue, measurement.Value);
    }

    [Fact]
    public void WhenComparing_ShouldBeEqual()
    {
        //Arrange
        var measurement1 = new MeasurementEntryEntityFaker().Generate();
        var measurement2 = new MeasurementEntryEntityFaker().Generate();
        
        //Act
        measurement2.ChangeName(measurement1.Name);
        measurement2.ChangeValue(measurement1.Value);
        
        //Assert
        Assert.True(measurement1.Equals(measurement2));
    }

    [Fact]
    public void WhenComparing_ShouldNotBeEqual()
    {
        //Arrange
        var measurement1 = new MeasurementEntryEntityFaker().Generate();
        var measurement2 = new MeasurementEntryEntityFaker().Generate();

        //Act
        measurement2.ChangeName(new Faker().Lorem.Word());
        measurement2.ChangeValue(new Faker().Random.Double(max: 200));

        //Assert
        Assert.False(measurement1.Equals(measurement2));
    }
}
