using Ambev.DeveloperEvaluation.Domain.Products;
using Ambev.DeveloperEvaluation.Integration.WebApi.Features.Products.TestData;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProducts;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.WebApi.Features.Products;

/// <summary>
/// Contains integration tests for the product feature.
/// </summary>
public class CreateProductTests : IClassFixture<IntegrationTestWebApiFactory>, IDisposable
{
    private bool _disposed;

    private readonly IServiceScope _scope;
    private readonly ISender _sender;
    private readonly IProductRepository _productRepository;

    public CreateProductTests(IntegrationTestWebApiFactory factory)
    {
        _scope = factory.Services.CreateScope();

        _sender = _scope.ServiceProvider.GetRequiredService<ISender>();
        _productRepository = _scope.ServiceProvider.GetRequiredService<IProductRepository>();
    }

    [Fact(DisplayName = "CreateProductCommand must create a new product")]
    public async Task Product_CreateProductCommand_Should_Create_Product()
    {
        // Arrange
        var command = CreateProductCommandTestData.GenerateValidProductCommand();

        // Act
        var result = await _sender.Send(command);

        // Assert
        var product = await _productRepository.GetByIdAsync(result.Id);

        Assert.NotNull(product);

        Assert.Equal(command.Title, product.Title);
        Assert.Equal(command.Price, product.Price);
        Assert.Equal(command.Description, product.Description);
        Assert.Equal(command.Category, product.Category);
        Assert.Equal(command.Image, product.Image);
        Assert.Equal(command.Quantity, product.Quantity);
        Assert.Equal(Math.Round(command.Rating.Rate, 2), product.Rating.Rate);
        Assert.Equal(command.Rating.Count, product.Rating.Count);

        Assert.Null(product.UpdatedAt);
    }

    [Fact(DisplayName = "CreateProductCommand must create new products and get with filter must return first product")]
    public async Task Product_Multiple_CreateProductCommand_Should_Create_Products_And_Get_Must_Return_First_Product()
    {
        // Arrange
        var commands = new List<CreateProductCommand>
        {
            CreateProductCommandTestData.GenerateValidProductCommand(),
            CreateProductCommandTestData.GenerateValidProductCommand(),
            CreateProductCommandTestData.GenerateValidProductCommand()
        };

        // Act
        foreach (var command in commands)
        {
            await _sender.Send(command);
        }

        var query = new Dictionary<string, string>
        {
            {"title", commands[0].Title},
            {"category", commands[0].Category},
        };

        var queryCommand = new GetProductsCommand(query);

        var products = await _sender.Send(queryCommand);

        // Assert
        Assert.NotNull(products);

        Assert.Equal(1, products.Page);
        Assert.Equal(10, products.Size);
        Assert.Equal(1, products.TotalItems);
        Assert.Equal(1, products.TotalPages);

        Assert.Single(products.Items);

        Assert.Equal(commands[0].Title, products.Items[0].Title);
        Assert.Equal(commands[0].Price, products.Items[0].Price);
        Assert.Equal(commands[0].Description, products.Items[0].Description);
        Assert.Equal(commands[0].Category, products.Items[0].Category);
        Assert.Equal(commands[0].Image, products.Items[0].Image);
        Assert.Equal(Math.Round(commands[0].Rating.Rate, 2), products.Items[0].Rating.Rate);
        Assert.Equal(commands[0].Rating.Count, products.Items[0].Rating.Count);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _scope?.Dispose();
            }

            _disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
