using System;
using Vestis._03_Domain.Entities;
using Xunit;

namespace Vestis._03_Domain.tests.Entities;

public class ProductEntityTests
{
    // [AI Generated] Método gerado automaticamente pelo agente desenvolvedor.
    [Fact]
    public void ProductEntity_HappyPath_DeveCriarInstanciaCorretamenteEAtribuirPropriedadesQuandoParametrosValidosSaoFornecidos()
    {
        // Arrange
        var studio = new StudioEntity("Test Studio");
        string name = "Test Product";
        string category = "Category A";
        string description = "Test Description";
        double price = 99.99;
        int unitCount = 10;
        string imgUrl = "http://test.url/img.png";

        // Act
        var product = new ProductEntity(studio, name, category, description, price, unitCount, imgUrl);

        // Assert
        Assert.NotNull(product);
        Assert.Equal(studio, product.Studio);
        Assert.Equal(studio.Id, product.StudioId);
        Assert.Equal(name, product.Name);
        Assert.Equal(category, product.Category);
        Assert.Equal(description, product.Description);
        Assert.Equal(price, product.Price);
        Assert.Equal(unitCount, product.UnitCount);
        Assert.Equal(imgUrl, product.ImgUrl);
    }

    // [AI Generated] Método gerado automaticamente pelo agente desenvolvedor.
    [Fact]
    public void UpdateAmmount_HappyPath_DeveAumentarAQuantidadeUnitCountQuandoOValorFornecidoForMaiorQueZero()
    {
        // Arrange
        var studio = new StudioEntity("Test Studio");
        var product = new ProductEntity(studio, "Test Product", "Category", unitCount: 10);
        int quantityToAdd = 5;
        int expectedUnitCount = 15;

        // Act
        product.UpdateAmmount(quantityToAdd);

        // Assert
        Assert.Equal(expectedUnitCount, product.UnitCount);
    }

    // [AI Generated] Método gerado automaticamente pelo agente desenvolvedor.
    [Fact]
    public void UpdateAmmount_HappyPath_DeveDiminuirAQuantidadeUnitCountQuandoOValorFornecidoForMenorQueZeroDesdeQueOValorAbsolutoSejaMenorOuIgualAoEstoqueAtual()
    {
        // Arrange
        var studio = new StudioEntity("Test Studio");
        var product = new ProductEntity(studio, "Test Product", "Category", unitCount: 10);
        int quantityToRemove = -5;
        int expectedUnitCount = 5;

        // Act
        product.UpdateAmmount(quantityToRemove);

        // Assert
        Assert.Equal(expectedUnitCount, product.UnitCount);
    }

    // [AI Generated] Método gerado automaticamente pelo agente desenvolvedor.
    [Fact]
    public void UpdateAmmount_ExcecaoEsperada_DeveLancarInvalidOperationExceptionQuandoOValorFornecidoForNegativoESeuValorAbsolutoForMaiorQueOEstoqueAtual()
    {
        // Arrange
        var studio = new StudioEntity("Test Studio");
        var product = new ProductEntity(studio, "Test Product", "Category", unitCount: 10);
        int quantityToRemove = -15;

        // Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(() => product.UpdateAmmount(quantityToRemove));
        Assert.Equal("Insufficient stock to reduce by the specified quantity.", exception.Message);
    }
}
