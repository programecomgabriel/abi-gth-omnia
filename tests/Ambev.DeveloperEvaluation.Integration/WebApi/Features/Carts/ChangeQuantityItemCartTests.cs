using Ambev.DeveloperEvaluation.Domain.Carts;
using Ambev.DeveloperEvaluation.Integration.WebApi.Features.Carts.TestData;
using Ambev.DeveloperEvaluation.Integration.WebApi.Features.Products.TestData;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.ChangeQuantityItemCart;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.WebApi.Features.Carts;

/// <summary>
/// Contains integration tests for the change quantity item cart feature.
/// </summary>
public class ChangeQuantityItemCartTests : IClassFixture<IntegrationTestWebApiFactory>, IDisposable
{
    private bool _disposed;

    private readonly IServiceScope _scope;
    private readonly ISender _sender;
    private readonly ICartRepository _cartRepository;
    private readonly DefaultContext _defaultContext;

    public ChangeQuantityItemCartTests(IntegrationTestWebApiFactory factory)
    {
        _scope = factory.Services.CreateScope();

        _sender = _scope.ServiceProvider.GetRequiredService<ISender>();
        _cartRepository = _scope.ServiceProvider.GetRequiredService<ICartRepository>();
        _defaultContext = _scope.ServiceProvider.GetRequiredService<DefaultContext>();
    }

    [Fact(DisplayName = "ChangeQuantityItemCart should change quantity of an item of a cart")]
    public async Task Cart_ChangeQuantityItemCart_Should_Change_Quantity_Item_Cart()
    {
        // Arrange Product
        var createProductCommand = CreateProductCommandTestData.GenerateValidCreateProductCommand();
        var createProductResult = await _sender.Send(createProductCommand);

        // Arrange Cart
        var createCartCommand = CreateCartCommandTestData.GenerateValidCreateCartCommand();
        createCartCommand.ProductId = createProductResult.Id;
        createCartCommand.Quantity = 3;
        var createCartResult = await _sender.Send(createCartCommand);

        // Arrange
        var command = new ChangeQuantityItemCartCommand()
        {
            Id = createCartResult.Id,
            ProductId = createProductResult.Id,
            Quantity = 10
        };

        // clear change tracker to avoid side effects from previous operations
        _defaultContext.ChangeTracker.Clear();

        // Act
        var result = await _sender.Send(command);

        // Assert
        var cart = await _cartRepository.GetByIdAsync(result.Id, createCartResult.UserId);

        Assert.NotNull(cart);
        Assert.Equal(10, cart.Items.First().Quantity);
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
