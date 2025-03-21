namespace Vestis._03_Domain.tests.Tests;

public class StudioMembershipEntityTests
{
    [Fact]
    public void ChangeRole_ShouldChangeRole()
    {
        // Arrange
        var membership = new StudioMembershipEntityFaker().Generate();
        var newRole = new RoleEntityFaker().Generate();

        //Act
        membership.ChangeRole(newRole);

        //Assert
        Assert.Equal(newRole, membership.Role);
    }
}
