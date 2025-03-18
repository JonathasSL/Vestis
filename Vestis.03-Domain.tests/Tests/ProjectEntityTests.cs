namespace Vestis._03_Domain.tests.Tests;

public class ProjectEntityTests
{
    [Fact]
    public void ShouldCreateProject()
    {
        // Arrange
        var projectName = new Faker().Name.FullName();
        // Act
        var project = new ProjectEntity(projectName);

        // Assert
        Assert.Equal(projectName, project.Name);
    }

    [Fact]
    public void ShouldChangeProjectName()
    {
        // Arrange
        var project = new ProjectEntityFaker().Generate();
        var newName = new Faker().Name.FullName();
        
        // Act
        project.ChangeName(newName);
        
        // Assert
        Assert.Equal(newName, project.Name);
    }

    [Fact]
    public void ShouldChangeProjectDescription()
    {
        // Arrange
        var project = new ProjectEntityFaker().Generate();
        var newDescription = new Faker().Lorem.Sentence();

        // Act
        project.ChangeDescription(newDescription);

        // Assert
        Assert.Equal(newDescription, project.Description);
    }

    [Fact]
    public void ShouldChangeClient()
    {
        // Arrange
        var project = new ProjectEntityFaker().Generate();
        var newClient = new ClientEntityFaker().Generate();

        // Act
        project.ChangeClient(newClient);

        // Assert
        Assert.Equal(newClient, project.Client);
    }

    [Fact]
    public void ShouldAddBodyMeasurement()
    {
        // Arrange
        var project = new ProjectEntityFaker().Generate();
        var bodyMeasurement = new BodyMeasurementEntityFaker().Generate();

        // Act
        project.AddBodyMeasurement(bodyMeasurement);

        // Assert
        Assert.Contains(bodyMeasurement, project.BodyMeasurements);
    }
}
