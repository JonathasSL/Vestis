namespace Vestis._03_Domain.tests.Tests;

public class ClientEntityTests
{
    [Fact]
    public void ShouldCreateClient()
    {

        // Arrange
        var clientName = new Faker().Name.FullName();

        // Act
        var client = new ClientEntity(clientName);

        // Assert
        Assert.Equal(clientName, client.Name);
    }

    [Fact]
    public void ShouldChangeName()
    {
        // Arrange
        var client = new ClientEntityFaker().Generate();
        var newName = new Faker().Name.FullName();
        
        // Act
        client.ChangeName(newName);
        
        // Assert
        Assert.Equal(newName, client.Name);
    }
}
