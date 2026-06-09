using System;
using Xunit;
using Vestis._03_Domain.Entities;

namespace Vestis._03_Domain.tests.Entities;

public class RoleEntityTests
{
    // [AI Generated] Método gerado automaticamente pelo agente desenvolvedor.
    [Fact]
    public void RoleEntity_HappyPath_InitializesNameAndDescription()
    {
        // Arrange
        string expectedName = "RoleName";
        string expectedDescription = "RoleDescription";

        // Act
        var role = new RoleEntity(expectedName, expectedDescription, null);

        // Assert
        Assert.Equal(expectedName, role.Name);
        Assert.Equal(expectedDescription, role.Description);
    }

    // [AI Generated] Método gerado automaticamente pelo agente desenvolvedor.
    [Fact]
    public void RoleEntity_EdgeCase_StudioParameterIsIgnored()
    {
        // Arrange
        string expectedName = "RoleName";
        string expectedDescription = "RoleDescription";

        // Act
        // Even if studio is null, the instance is created without throwing.
        var role = new RoleEntity(expectedName, expectedDescription, null);

        // Assert
        Assert.NotNull(role);
        Assert.Equal(expectedName, role.Name);
        Assert.Equal(expectedDescription, role.Description);
    }

    // [AI Generated] Método gerado automaticamente pelo agente desenvolvedor.
    [Fact]
    public void AddPermission_FirstPermission_InitializesHashSetAndReturnsTrue()
    {
        // Arrange
        var role = new RoleEntity("RoleName", "RoleDescription", null);
        var permission = new PermissionEntity("Perm1", "Desc1");

        // Act
        bool result = role.AddPermission(permission);

        // Assert
        Assert.True(result);
        Assert.NotNull(role.Permissions);
        Assert.Single(role.Permissions);
        Assert.Contains(permission, role.Permissions);
    }

    // [AI Generated] Método gerado automaticamente pelo agente desenvolvedor.
    [Fact]
    public void AddPermission_SubsequentPermission_AddsToExistingHashSetAndReturnsTrue()
    {
        // Arrange
        var role = new RoleEntity("RoleName", "RoleDescription", null);
        var permission1 = new PermissionEntity("Perm1", "Desc1");
        var permission2 = new PermissionEntity("Perm2", "Desc2");
        role.AddPermission(permission1); // Initialize HashSet and add first

        // Act
        bool result = role.AddPermission(permission2);

        // Assert
        Assert.True(result);
        Assert.Equal(2, role.Permissions.Count);
        Assert.Contains(permission2, role.Permissions);
    }

    // [AI Generated] Método gerado automaticamente pelo agente desenvolvedor.
    [Fact]
    public void AddPermission_DuplicatePermission_ReturnsFalse()
    {
        // Arrange
        var role = new RoleEntity("RoleName", "RoleDescription", null);
        var permission = new PermissionEntity("Perm1", "Desc1");
        role.AddPermission(permission);

        // Act
        bool result = role.AddPermission(permission);

        // Assert
        Assert.False(result);
        Assert.Single(role.Permissions);
    }
}
