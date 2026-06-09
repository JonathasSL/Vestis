using System;
using Vestis._03_Domain.Entities;
using Xunit;

namespace Vestis._03_Domain.tests.Entities;

public class EmailVerificationTokenEntityTests
{
    // [AI Generated] Método gerado automaticamente pelo agente desenvolvedor.
    [Fact]
    public void Constructor_HappyPath_InitializesProperties()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var token = "some-token";
        var expirationDateUtc = DateTime.UtcNow.AddDays(1);

        // Act
        var entity = new EmailVerificationTokenEntity(userId, token, expirationDateUtc);

        // Assert
        Assert.Equal(userId, entity.UserId);
        Assert.Equal(token, entity.Token);
        Assert.Equal(expirationDateUtc, entity.ExpirationDateUtc);
        Assert.False(entity.IsUsed);
    }

    // [AI Generated] Método gerado automaticamente pelo agente desenvolvedor.
    [Fact]
    public void IsExpired_HappyPath_FutureDate_ReturnsFalse()
    {
        // Arrange
        var expirationDateUtc = DateTime.UtcNow.AddMinutes(5);
        var entity = new EmailVerificationTokenEntity(Guid.NewGuid(), "token", expirationDateUtc);

        // Act
        var result = entity.IsExpired();

        // Assert
        Assert.False(result);
    }

    // [AI Generated] Método gerado automaticamente pelo agente desenvolvedor.
    [Fact]
    public void IsExpired_EdgeCase_PastDate_ReturnsTrue()
    {
        // Arrange
        var expirationDateUtc = DateTime.UtcNow.AddMinutes(-5);
        var entity = new EmailVerificationTokenEntity(Guid.NewGuid(), "token", expirationDateUtc);

        // Act
        var result = entity.IsExpired();

        // Assert
        Assert.True(result);
    }

    // [AI Generated] Método gerado automaticamente pelo agente desenvolvedor.
    [Fact]
    public void Use_HappyPath_NotUsedAndNotExpired_ReturnsTrueAndSetsIsUsed()
    {
        // Arrange
        var expirationDateUtc = DateTime.UtcNow.AddMinutes(5);
        var entity = new EmailVerificationTokenEntity(Guid.NewGuid(), "token", expirationDateUtc);

        // Act
        var result = entity.Use();

        // Assert
        Assert.True(result);
        Assert.True(entity.IsUsed);
    }

    // [AI Generated] Método gerado automaticamente pelo agente desenvolvedor.
    [Fact]
    public void Use_EdgeCase_AlreadyUsed_ReturnsFalse()
    {
        // Arrange
        var expirationDateUtc = DateTime.UtcNow.AddMinutes(5);
        var entity = new EmailVerificationTokenEntity(Guid.NewGuid(), "token", expirationDateUtc);
        
        // Act to set to used first
        entity.Use();
        
        // Act again
        var result = entity.Use();

        // Assert
        Assert.False(result);
        Assert.True(entity.IsUsed);
    }

    // [AI Generated] Método gerado automaticamente pelo agente desenvolvedor.
    [Fact]
    public void Use_EdgeCase_Expired_ReturnsFalse()
    {
        // Arrange
        var expirationDateUtc = DateTime.UtcNow.AddMinutes(-5);
        var entity = new EmailVerificationTokenEntity(Guid.NewGuid(), "token", expirationDateUtc);

        // Act
        var result = entity.Use();

        // Assert
        Assert.False(result);
        Assert.False(entity.IsUsed);
    }
}
