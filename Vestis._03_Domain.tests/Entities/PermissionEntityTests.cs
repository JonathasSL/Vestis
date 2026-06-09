using System;
using Xunit;
using Vestis._03_Domain.Entities;

namespace Vestis._03_Domain.tests.Entities;

public class PermissionEntityTests
{
    // [AI Generated] Método gerado automaticamente pelo agente desenvolvedor.
    [Fact]
    public void Constructor_ValidNameAndDescription_InitializesProperties()
    {
        // Arrange
        string expectedName = "ValidName";
        string expectedDescription = "ValidDescription";

        // Act
        var permission = new PermissionEntity(expectedName, expectedDescription);

        // Assert
        Assert.Equal(expectedName, permission.Name);
        Assert.Equal(expectedDescription, permission.Description);
    }

    // [AI Generated] Método gerado automaticamente pelo agente desenvolvedor.
    [Fact]
    public void Constructor_EmptyStrings_MaintainsStateWithoutExceptions()
    {
        // Arrange
        string expectedName = string.Empty;
        string expectedDescription = string.Empty;

        // Act
        var permission = new PermissionEntity(expectedName, expectedDescription);

        // Assert
        Assert.Equal(expectedName, permission.Name);
        Assert.Equal(expectedDescription, permission.Description);
    }

    // [AI Generated] Método gerado automaticamente pelo agente desenvolvedor.
    [Fact]
    public void Constructor_NullValues_AssignsNullToProperties()
    {
        // Arrange
        string expectedName = null;
        string expectedDescription = null;

        // Act
        var permission = new PermissionEntity(expectedName, expectedDescription);

        // Assert
        Assert.Null(permission.Name);
        Assert.Null(permission.Description);
    }
}
