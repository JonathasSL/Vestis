namespace Vestis._03_Domain.tests.Tests;

public class UserEntityTests
{
    [Fact]
    public void ShouldChangeName()
    {
        // Arrange
        var user = new UserEntityFaker().Generate();
        var newName = new Faker().Name.FullName();

        // Act
        user.ChangeName(newName);

        // Assert
        Assert.Equal(newName, user.Name);
    }

    [Fact]
    public void ShouldChangeEmail()
    {
        //Arrange
        var user = new UserEntityFaker().Generate();
        var newEmail = new Faker().Internet.Email();

        //Act
        user.ChangeEmail(newEmail);

        //Assert
        Assert.Equal(newEmail, user.Email);
    }

    [Fact]
    public void ShouldChangePassword()
    {
        //Arrange
        var user = new UserEntityFaker().Generate();
        var newPassword = new Faker().Internet.Password();
        
        //Act
        user.ChangePassword(newPassword);
        
        //Assert
        Assert.Equal(newPassword, user.Password);
    }
}
