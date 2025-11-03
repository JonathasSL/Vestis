namespace Vestis._03_Domain.tests.Tests;

public class RoleEntityTests
{
    [Fact]
    public void AddPermission_WithValidData_ShouldAddPermission()
    {
        // Arrange
        var permission = new PermissionEntityFaker().Generate();
        var role = new RoleEntityFaker().Generate();

        // Act
        role.AddPermission(permission);

        // Assert
        Assert.Contains(permission, role.Permissions);
    }
}
