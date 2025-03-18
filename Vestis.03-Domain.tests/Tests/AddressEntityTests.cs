namespace Vestis._03_Domain.tests.Tests;

public class AddressEntityTests
{
    [Fact]
    public void ShouldChangeStreet()
    {
        // Arrange
        var address = new AddressEntityFaker().Generate();
        var newStreet = new Faker().Address.StreetName();
        // Act
        address.ChangeStreet(newStreet);
        // Assert
        Assert.Equal(newStreet, address.Street);
    }
    
    [Fact]
    public void ShouldChangeNumber()
    {
        // Arrange
        var address = new AddressEntityFaker().Generate();
        var newNumber = new Faker().Address.BuildingNumber();
        // Act
        address.ChangeNumber(newNumber);
        // Assert
        Assert.Equal(newNumber, address.Number);
    }

    [Fact]
    public void ShouldChangeComplement()
    {
        // Arrange
        var address = new AddressEntityFaker().Generate();
        var newComplement = new Faker().Lorem.Word();
        // Act
        address.ChangeComplement(newComplement);
        // Assert
        Assert.Equal(newComplement, address.Complement);
    }

    [Fact]
    public void ShouldChangeNeighborhood()
    {
        // Arrange
        var address = new AddressEntityFaker().Generate();
        var newNeighborhood = new Faker().Lorem.Word();
        // Act
        address.ChangeNeighborhood(newNeighborhood);
        // Assert
        Assert.Equal(newNeighborhood, address.Neighborhood);
    }

    [Fact]
    public void ShouldChangeCity()
    {
        // Arrange
        var address = new AddressEntityFaker().Generate();
        var newCity = new Faker().Address.City();
        // Act
        address.ChangeCity(newCity);
        // Assert
        Assert.Equal(newCity, address.City);
    }

    [Fact]
    public void ShouldChangeState()
    {
        // Arrange
        var address = new AddressEntityFaker().Generate();
        var newState = new Faker().Address.State();
        // Act
        address.ChangeState(newState);
        // Assert
        Assert.Equal(newState, address.State);
    }

    [Fact]
    public void ShouldChangeCountry()
    {
        // Arrange
        var address = new AddressEntityFaker().Generate();
        var newCountry = new Faker().Address.Country();
        // Act
        address.ChangeCountry(newCountry);
        // Assert
        Assert.Equal(newCountry, address.Country);
    }    
    
    [Fact]
    public void ShouldChangeZipCode()
    {
        // Arrange
        var address = new AddressEntityFaker().Generate();
        var newZipCode = new Faker().Address.ZipCode();
        // Act
        address.ChangeZipCode(newZipCode);
        // Assert
        Assert.Equal(newZipCode, address.ZipCode);
    }
}
