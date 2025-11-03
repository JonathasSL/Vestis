namespace Vestis._03_Domain.tests.Tests;

public class StudioEntityTests
{
    [Fact]
    public void Constructor_ShouldInitializeWithValidName()
    {
        // Arrange & Act
        var studioName = new Faker().Company.CompanyName();
        var studio = new StudioEntity(studioName);

        Assert.Equal(studioName, studio.Name);
    }

    [Fact]
    public void ChangeName_ShouldUpdateName_WhenValid()
    {
        // Arrange
        var studio = new StudioEntityFaker().Generate();
        var newName = new Faker().Company.CompanyName();

        // Act
        studio.ChangeName(newName);

        // Assert
        Assert.Equal(newName, studio.Name);
    }

    [Fact]
    public void ChangeName_ShouldThrowException_WhenNameIsInvalid()
    {
        // Arrange
        var studio = new StudioEntityFaker().Generate();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => studio.ChangeName(""));
    }

    [Fact]
    public void ChangeContactEmail_ShouldUpdateEmail_WhenValid()
    {
        // Arrange
        var studio = new StudioEntityFaker().Generate();
        var contactEmail = new Faker().Internet.Email();

        // Act
        studio.ChangeContactEmail(contactEmail);

        // Assert
        Assert.Equal(contactEmail, studio.ContactEmail);
    }

    [Fact]
    public void ChangePhoneNumber_ShouldUpdatePhoneNumber_WhenValid()
    {
        // Arrange
        var studio = new StudioEntityFaker().Generate();
        var phoneNumber = new Faker().Phone.PhoneNumber();

        // Act
        studio.ChangePhoneNumber(phoneNumber);

        // Assert
        Assert.Equal(phoneNumber, studio.PhoneNumber);
    }

    [Fact]
    public void ChangeAddress_ShouldUpdateAddress_WhenDifferent()
    {
        // Arrange
        var address = new AddressEntityFaker().Generate();
        var studio = new StudioEntityFaker().Generate();

        // Act
        studio.ChangeAddress(address);

        // Assert
        Assert.Equal(address, studio.Address);
    }

    /*
    [Fact]
    public void AddClient_ShouldAddClientToList()
    {
        // Arrange
        var studio = new StudioEntityFaker().Generate();
        var client = new ClientEntityFaker().Generate();

        // Act
        studio.AddClient(client);

        // Assert
        Assert.NotEmpty(studio.Clients);
        Assert.Contains(client, studio.Clients);
    }
    */
}
