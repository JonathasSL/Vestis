using System;
using Xunit;
using Vestis._03_Domain.Entities;

namespace Vestis._03_Domain.tests.Entities;

public class StudioMembershipEntityTests
{
    // [AI Generated] Método gerado automaticamente pelo agente desenvolvedor.
    [Fact]
    public void StudioMembershipEntity_HappyPath_InitializesProperties()
    {
        // Arrange
        var user = new UserEntity("Name", "email@test.com", "password");
        var studio = new StudioEntity("Studio Name");
        var roleName = "Admin";

        // Act
        var membership = new StudioMembershipEntity(user, roleName, studio);

        // Assert
        Assert.Equal(user.Id, membership.UserId);
        Assert.Equal(user, membership.User);
        Assert.Equal(roleName, membership.Role);
        Assert.Equal(studio.Id, membership.StudioId);
        Assert.Equal(studio, membership.Studio);
    }

    // [AI Generated] Método gerado automaticamente pelo agente desenvolvedor.
    [Fact]
    public void ChangeRole_HappyPath_UpdatesRoleAndSetsUpdatedDate()
    {
        // Arrange
        var user = new UserEntity("Name", "email@test.com", "password");
        var studio = new StudioEntity("Studio Name");
        var membership = new StudioMembershipEntity(user, "OldRole", studio);

        // Act
        membership.ChangeRole("NewRole");

        // Assert
        Assert.Equal("NewRole", membership.Role);
        Assert.NotNull(membership.UpdatedDate);
    }

    // [AI Generated] Método gerado automaticamente pelo agente desenvolvedor.
    [Fact]
    public void ChangeRole_EdgeCase_SameRoleDoesNotUpdate()
    {
        // Arrange
        var user = new UserEntity("Name", "email@test.com", "password");
        var studio = new StudioEntity("Studio Name");
        var membership = new StudioMembershipEntity(user, "SameRole", studio);
        var originalUpdatedDate = membership.UpdatedDate; // null

        // Act
        membership.ChangeRole("SameRole");

        // Assert
        Assert.Equal("SameRole", membership.Role);
        Assert.Equal(originalUpdatedDate, membership.UpdatedDate);
    }
}
