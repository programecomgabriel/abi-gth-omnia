using Ambev.DeveloperEvaluation.Domain.Carts;
using Ambev.DeveloperEvaluation.Integration.WebApi.Features.Carts.TestData;
using Ambev.DeveloperEvaluation.Integration.WebApi.Features.Products.TestData;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.CancelItemCart;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.WebApi.Features.Carts;

/// <summary>
/// Contains integration tests for the cancel item cart feature.
/// </summary>
public class CancelItemCartTests : IClassFixture<IntegrationTestWebApiFactory>, IDisposable
{
    private bool _disposed;

    private readonly IServiceScope _scope;
    private readonly ISender _sender;
    private readonly ICartRepository _cartRepository;
    private readonly DefaultContext _defaultContext;

    public CancelItemCartTests(IntegrationTestWebApiFactory factory)
    {
        _scope = factory.Services.CreateScope();

        _sender = _scope.ServiceProvider.GetRequiredService<ISender>();
        _cartRepository = _scope.ServiceProvider.GetRequiredService<ICartRepository>();
        _defaultContext = _scope.ServiceProvider.GetRequiredService<DefaultContext>();
    }

    [Fact(DisplayName = "CancelItemCartCommand should cancel an single item and a cart itself")]
    public async Task Cart_CancelItemCartCommand_Should_Cancel_Item_And_Cart()
    {
        // Arrange Product
        var createProductCommand = CreateProductCommandTestData.GenerateValidCreateProductCommand();
        var createProductResult = await _sender.Send(createProductCommand);

        // Arrange Cart
        var createCartCommand = CreateCartCommandTestData.GenerateValidCreateCartCommand();
        createCartCommand.ProductId = createProductResult.Id;
        createCartCommand.Quantity = 1;
        var createCartResult = await _sender.Send(createCartCommand);

        // Arrange
        var command = new CancelItemCartCommand()
        {
            Id = createCartResult.Id,
            ProductId = createProductResult.Id
        };

        // clear change tracker to avoid side effects from previous operations
        _defaultContext.ChangeTracker.Clear();

        // Act
        var result = await _sender.Send(command);

        // Assert
        var cart = await _cartRepository.GetByIdAsync(result.Id, createCartResult.UserId);

        Assert.NotNull(cart);
        Assert.True(cart.Cancelled);

        Assert.All(cart.Items, i => Assert.True(i.Cancelled));
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
