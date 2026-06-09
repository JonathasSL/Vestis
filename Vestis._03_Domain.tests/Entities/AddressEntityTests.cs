using System;
using Vestis._03_Domain.Entities;
using Xunit;

namespace Vestis._03_Domain.Tests.Entities;

public class AddressEntityTests
{
    private AddressEntity CreateAddressEntity()
    {
        return new AddressEntity("Rua Teste", "123", "Bairro Teste", "Cidade Teste", "TS", "12345-678");
    }

    // [AI Generated] Método gerado automaticamente pelo agente desenvolvedor.
    [Fact]
    public void ChangeStreet_HappyPath_ShouldUpdateStreet()
    {
        // Arrange
        var address = CreateAddressEntity();
        var newStreet = "Rua Nova";

        // Act
        address.ChangeStreet(newStreet);

        // Assert
        Assert.Equal(newStreet, address.Street);
    }

    // [AI Generated] Método gerado automaticamente pelo agente desenvolvedor.
    [Fact]
    public void ChangeStreet_EdgeCaseSameStreet_ShouldNotUpdateStreet()
    {
        // Arrange
        var address = CreateAddressEntity();
        var sameStreet = address.Street;

        // Act
        address.ChangeStreet(sameStreet);

        // Assert
        Assert.Equal(sameStreet, address.Street);
    }

    // [AI Generated] Método gerado automaticamente pelo agente desenvolvedor.
    [Fact]
    public void ChangeNumber_HappyPath_ShouldUpdateNumber()
    {
        // Arrange
        var address = CreateAddressEntity();
        var newNumber = "456";

        // Act
        address.ChangeNumber(newNumber);

        // Assert
        Assert.Equal(newNumber, address.Number);
    }

    // [AI Generated] Método gerado automaticamente pelo agente desenvolvedor.
    [Fact]
    public void ChangeNumber_EdgeCaseSameNumber_ShouldNotUpdateNumber()
    {
        // Arrange
        var address = CreateAddressEntity();
        var sameNumber = address.Number;

        // Act
        address.ChangeNumber(sameNumber);

        // Assert
        Assert.Equal(sameNumber, address.Number);
    }

    // [AI Generated] Método gerado automaticamente pelo agente desenvolvedor.
    [Fact]
    public void ChangeComplement_HappyPath_ShouldUpdateComplement()
    {
        // Arrange
        var address = CreateAddressEntity();
        var newComplement = "Apto 101";

        // Act
        address.ChangeComplement(newComplement);

        // Assert
        Assert.Equal(newComplement, address.Complement);
    }

    // [AI Generated] Método gerado automaticamente pelo agente desenvolvedor.
    [Fact]
    public void ChangeComplement_EdgeCaseSameComplement_ShouldNotUpdateComplement()
    {
        // Arrange
        var address = CreateAddressEntity();
        address.ChangeComplement("Apto 101");
        var sameComplement = address.Complement;

        // Act
        address.ChangeComplement(sameComplement);

        // Assert
        Assert.Equal(sameComplement, address.Complement);
    }

    // [AI Generated] Método gerado automaticamente pelo agente desenvolvedor.
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void ChangeComplement_EdgeCaseNullOrEmpty_ShouldUpdateComplement(string newComplement)
    {
        // Arrange
        var address = CreateAddressEntity();

        // Act
        address.ChangeComplement(newComplement);

        // Assert
        Assert.Equal(newComplement, address.Complement);
    }

    // [AI Generated] Método gerado automaticamente pelo agente desenvolvedor.
    [Fact]
    public void ChangeNeighborhood_HappyPath_ShouldUpdateNeighborhood()
    {
        // Arrange
        var address = CreateAddressEntity();
        var newNeighborhood = "Bairro Novo";

        // Act
        address.ChangeNeighborhood(newNeighborhood);

        // Assert
        Assert.Equal(newNeighborhood, address.Neighborhood);
    }

    // [AI Generated] Método gerado automaticamente pelo agente desenvolvedor.
    [Fact]
    public void ChangeNeighborhood_EdgeCaseSameNeighborhood_ShouldNotUpdateNeighborhood()
    {
        // Arrange
        var address = CreateAddressEntity();
        var sameNeighborhood = address.Neighborhood;

        // Act
        address.ChangeNeighborhood(sameNeighborhood);

        // Assert
        Assert.Equal(sameNeighborhood, address.Neighborhood);
    }

    // [AI Generated] Método gerado automaticamente pelo agente desenvolvedor.
    [Fact]
    public void ChangeCity_HappyPath_ShouldUpdateCity()
    {
        // Arrange
        var address = CreateAddressEntity();
        var newCity = "Cidade Nova";

        // Act
        address.ChangeCity(newCity);

        // Assert
        Assert.Equal(newCity, address.City);
    }

    // [AI Generated] Método gerado automaticamente pelo agente desenvolvedor.
    [Fact]
    public void ChangeCity_EdgeCaseSameCity_ShouldNotUpdateCity()
    {
        // Arrange
        var address = CreateAddressEntity();
        var sameCity = address.City;

        // Act
        address.ChangeCity(sameCity);

        // Assert
        Assert.Equal(sameCity, address.City);
    }

    // [AI Generated] Método gerado automaticamente pelo agente desenvolvedor.
    [Fact]
    public void ChangeState_HappyPath_ShouldUpdateState()
    {
        // Arrange
        var address = CreateAddressEntity();
        var newState = "NS";

        // Act
        address.ChangeState(newState);

        // Assert
        Assert.Equal(newState, address.State);
    }

    // [AI Generated] Método gerado automaticamente pelo agente desenvolvedor.
    [Fact]
    public void ChangeState_EdgeCaseSameState_ShouldNotUpdateState()
    {
        // Arrange
        var address = CreateAddressEntity();
        var sameState = address.State;

        // Act
        address.ChangeState(sameState);

        // Assert
        Assert.Equal(sameState, address.State);
    }

    // [AI Generated] Método gerado automaticamente pelo agente desenvolvedor.
    [Fact]
    public void ChangeCountry_HappyPath_ShouldUpdateCountry()
    {
        // Arrange
        var address = CreateAddressEntity();
        var newCountry = "País Novo";

        // Act
        address.ChangeCountry(newCountry);

        // Assert
        Assert.Equal(newCountry, address.Country);
    }

    // [AI Generated] Método gerado automaticamente pelo agente desenvolvedor.
    [Fact]
    public void ChangeCountry_EdgeCaseSameCountry_ShouldNotUpdateCountry()
    {
        // Arrange
        var address = CreateAddressEntity();
        address.ChangeCountry("País Teste");
        var sameCountry = address.Country;

        // Act
        address.ChangeCountry(sameCountry);

        // Assert
        Assert.Equal(sameCountry, address.Country);
    }

    // [AI Generated] Método gerado automaticamente pelo agente desenvolvedor.
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void ChangeCountry_EdgeCaseNullOrEmpty_ShouldUpdateCountry(string newCountry)
    {
        // Arrange
        var address = CreateAddressEntity();

        // Act
        address.ChangeCountry(newCountry);

        // Assert
        Assert.Equal(newCountry, address.Country);
    }

    // [AI Generated] Método gerado automaticamente pelo agente desenvolvedor.
    [Fact]
    public void ChangeZipCode_HappyPath_ShouldUpdateZipCode()
    {
        // Arrange
        var address = CreateAddressEntity();
        var newZipCode = "87654-321";

        // Act
        address.ChangeZipCode(newZipCode);

        // Assert
        Assert.Equal(newZipCode, address.ZipCode);
    }

    // [AI Generated] Método gerado automaticamente pelo agente desenvolvedor.
    [Fact]
    public void ChangeZipCode_EdgeCaseSameZipCode_ShouldNotUpdateZipCode()
    {
        // Arrange
        var address = CreateAddressEntity();
        var sameZipCode = address.ZipCode;

        // Act
        address.ChangeZipCode(sameZipCode);

        // Assert
        Assert.Equal(sameZipCode, address.ZipCode);
    }

    // [AI Generated] Método gerado automaticamente pelo agente desenvolvedor.
    [Fact]
    public void Equals_HappyPath_ShouldReturnTrueForSameValues()
    {
        // Arrange
        var address1 = CreateAddressEntity();
        var address2 = new AddressEntity("Rua Teste", "123", "Bairro Teste", "Cidade Teste", "TS", "12345-678");

        // Act
        var result = address1.Equals(address2);

        // Assert
        Assert.True(result);
    }

    // [AI Generated] Método gerado automaticamente pelo agente desenvolvedor.
    [Fact]
    public void Equals_EdgeCaseDifferentProperty_ShouldReturnFalse()
    {
        // Arrange
        var address1 = CreateAddressEntity();
        var address2 = new AddressEntity("Rua Diferente", "123", "Bairro Teste", "Cidade Teste", "TS", "12345-678");

        // Act
        var result = address1.Equals(address2);

        // Assert
        Assert.False(result);
    }

    // [AI Generated] Método gerado automaticamente pelo agente desenvolvedor.
    [Fact]
    public void Equals_EdgeCaseNullObject_ShouldReturnFalse()
    {
        // Arrange
        var address = CreateAddressEntity();

        // Act
        var result = address.Equals(null);

        // Assert
        Assert.False(result);
    }

    // [AI Generated] Método gerado automaticamente pelo agente desenvolvedor.
    [Fact]
    public void Equals_EdgeCaseDifferentType_ShouldReturnFalse()
    {
        // Arrange
        var address = CreateAddressEntity();
        var otherObject = new object();

        // Act
        var result = address.Equals(otherObject);

        // Assert
        Assert.False(result);
    }
}