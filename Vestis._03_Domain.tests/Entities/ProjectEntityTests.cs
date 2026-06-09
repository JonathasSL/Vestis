using System;
using System.Reflection;
using Vestis._03_Domain.Entities;
using Xunit;

namespace Vestis._03_Domain.Tests.Entities;

public class ProjectEntityTests
{
    private StudioEntity CreateStudioEntity() => new StudioEntity("Studio Test");
    private ClientEntity CreateClientEntity() => new ClientEntity("Client Test");

    // [AI Generated] Método gerado automaticamente pelo agente desenvolvedor.
    [Fact]
    public void Constructor_HappyPath_ShouldInitializeProperties()
    {
        // Arrange
        var studio = CreateStudioEntity();
        var name = "Project Name";

        // Act
        var project = new ProjectEntity(studio, name);

        // Assert
        Assert.Equal(studio, project.Studio);
        Assert.Equal(studio.Id, project.StudioId);
        Assert.Equal(name, project.Name);
    }

    // [AI Generated] Método gerado automaticamente pelo agente desenvolvedor.
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_ExpectedException_ShouldThrowArgumentException_WhenNameIsNullOrWhiteSpace(string invalidName)
    {
        // Arrange
        var studio = CreateStudioEntity();

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new ProjectEntity(studio, invalidName));
    }

    // [AI Generated] Método gerado automaticamente pelo agente desenvolvedor.
    [Fact]
    public void Constructor_ExpectedException_ShouldThrowNullReferenceException_WhenStudioIsNull()
    {
        // Arrange
        StudioEntity nullStudio = null;
        var name = "Project Name";

        // Act & Assert
        Assert.Throws<NullReferenceException>(() => new ProjectEntity(nullStudio, name));
    }

    // [AI Generated] Método gerado automaticamente pelo agente desenvolvedor.
    [Fact]
    public void ChangeName_HappyPath_ShouldUpdateNameAndSetAsUpdated()
    {
        // Arrange
        var project = new ProjectEntity(CreateStudioEntity(), "Old Name");
        var newName = "New Name";

        // Act
        project.ChangeName(newName);

        // Assert
        Assert.Equal(newName, project.Name);
        Assert.NotNull(project.UpdatedDate);
    }

    // [AI Generated] Método gerado automaticamente pelo agente desenvolvedor.
    [Fact]
    public void ChangeName_EdgeCase_ShouldNotUpdateState_WhenNameIsIdentical()
    {
        // Arrange
        var project = new ProjectEntity(CreateStudioEntity(), "Same Name");
        var sameName = "Same Name";

        // Act
        project.ChangeName(sameName);

        // Assert
        Assert.Equal(sameName, project.Name);
        Assert.Null(project.UpdatedDate);
    }

    // [AI Generated] Método gerado automaticamente pelo agente desenvolvedor.
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void ChangeName_ExpectedException_ShouldThrowArgumentException_WhenNameIsNullOrWhiteSpace(string invalidName)
    {
        // Arrange
        var project = new ProjectEntity(CreateStudioEntity(), "Valid Name");

        // Act & Assert
        Assert.Throws<ArgumentException>(() => project.ChangeName(invalidName));
    }

    // [AI Generated] Método gerado automaticamente pelo agente desenvolvedor.
    [Fact]
    public void ChangeDescription_HappyPath_ShouldUpdateDescriptionAndSetAsUpdated()
    {
        // Arrange
        var project = new ProjectEntity(CreateStudioEntity(), "Project Name");
        var newDescription = "New Description";

        // Act
        project.ChangeDescription(newDescription);

        // Assert
        Assert.Equal(newDescription, project.Description);
        Assert.NotNull(project.UpdatedDate);
    }

    // [AI Generated] Método gerado automaticamente pelo agente desenvolvedor.
    [Fact]
    public void ChangeDescription_EdgeCase_ShouldNotUpdateState_WhenDescriptionIsIdentical()
    {
        // Arrange
        var project = new ProjectEntity(CreateStudioEntity(), "Project Name");
        project.ChangeDescription("Same Description");
        var initialUpdatedDate = project.UpdatedDate;

        // Act
        project.ChangeDescription("Same Description");

        // Assert
        Assert.Equal("Same Description", project.Description);
        Assert.Equal(initialUpdatedDate, project.UpdatedDate);
    }

    // [AI Generated] Método gerado automaticamente pelo agente desenvolvedor.
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void ChangeDescription_HappyPath_ShouldAllowNullOrEmptyDescription(string emptyDescription)
    {
        // Arrange
        var project = new ProjectEntity(CreateStudioEntity(), "Project Name");
        project.ChangeDescription("Initial Description");

        // Act
        project.ChangeDescription(emptyDescription);

        // Assert
        Assert.Equal(emptyDescription, project.Description);
    }

    // [AI Generated] Método gerado automaticamente pelo agente desenvolvedor.
    [Fact]
    public void ChangeClient_HappyPath_ShouldUpdateClientAndSetAsUpdated()
    {
        // Arrange
        var project = new ProjectEntity(CreateStudioEntity(), "Project Name");
        var initialClient = CreateClientEntity();
        
        typeof(ProjectEntity).GetProperty("Client", BindingFlags.Public | BindingFlags.Instance)
            ?.SetValue(project, initialClient);

        var newClient = CreateClientEntity();

        // Act
        project.ChangeClient(newClient);

        // Assert
        Assert.Equal(newClient, project.Client);
        Assert.Equal(newClient.Id, project.ClientId);
        Assert.NotNull(project.UpdatedDate);
    }

    // [AI Generated] Método gerado automaticamente pelo agente desenvolvedor.
    [Fact]
    public void ChangeClient_EdgeCase_ShouldNotUpdateState_WhenClientIsIdentical()
    {
        // Arrange
        var project = new ProjectEntity(CreateStudioEntity(), "Project Name");
        var client = CreateClientEntity();
        
        typeof(ProjectEntity).GetProperty("Client", BindingFlags.Public | BindingFlags.Instance)
            ?.SetValue(project, client);

        project.ChangeDescription("Setup update date");
        var initialUpdatedDate = project.UpdatedDate;

        // Act
        project.ChangeClient(client);

        // Assert
        Assert.Equal(client, project.Client);
        Assert.Equal(initialUpdatedDate, project.UpdatedDate);
    }

    // [AI Generated] Método gerado automaticamente pelo agente desenvolvedor.
    [Fact]
    public void ChangeClient_ExpectedException_ShouldThrowNullReferenceException_WhenClientIsInternalNull()
    {
        // Arrange
        var project = new ProjectEntity(CreateStudioEntity(), "Project Name");
        var newClient = CreateClientEntity();

        // Act & Assert
        Assert.Throws<NullReferenceException>(() => project.ChangeClient(newClient));
    }

    // [AI Generated] Método gerado automaticamente pelo agente desenvolvedor.
    [Fact]
    public void ChangeClient_ExpectedException_ShouldThrowNullReferenceException_WhenClientProvidedIsNull()
    {
        // Arrange
        var project = new ProjectEntity(CreateStudioEntity(), "Project Name");
        var initialClient = CreateClientEntity();
        
        typeof(ProjectEntity).GetProperty("Client", BindingFlags.Public | BindingFlags.Instance)
            ?.SetValue(project, initialClient);

        ClientEntity nullClient = null;

        // Act & Assert
        Assert.Throws<NullReferenceException>(() => project.ChangeClient(nullClient));
    }
}
