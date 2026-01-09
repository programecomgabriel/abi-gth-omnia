using Ambev.DeveloperEvaluation.Domain.Carts;
using Ambev.DeveloperEvaluation.Domain.Products;
using Ambev.DeveloperEvaluation.Domain.Users;
using Ambev.DeveloperEvaluation.WebApi.Common;
using MediatR;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart;

/// <summary>
/// Handler for processing <see cref="CreateCartCommand"/> requests.
/// </summary>
public class CreateCartHandler(
    Session session,
    IUserRepository userRepository,
    IProductRepository productRepository,
    ICartRepository cartRepository) : IRequestHandler<CreateCartCommand, CreateCartResult>
{
    /// <summary>
    /// Handles the CreateCartCommand request
    /// </summary>
    /// <param name="command">The CreateCart command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created cart details</returns>
    public async Task<CreateCartResult> Handle(CreateCartCommand command, CancellationToken cancellationToken)
    {
        var sessionUser = session.GetUser();

        if (!Guid.TryParse(sessionUser.Id, out var userId))
        {
            throw new InvalidOperationException("Invalid user ID in session.");
        }

        var user = await userRepository.GetByIdAsync(userId, cancellationToken) ?? throw new InvalidOperationException("User not found.");

        var product = await productRepository.GetByIdAsync(command.ProductId!.Value, cancellationToken) ?? throw new InvalidOperationException("Product not found.");

        var cartItem = new CartItem(product.Id, product.Price, command.Quantity!.Value);

        var cart = new Cart(command.Branch, user.Id, cartItem);

        await cartRepository.CreateAsync(cart, cancellationToken);

        return new CreateCartResult
        {
            Id = cart.Id,
            Branch = cart.Branch,
            UserId = cart.UserId,
            Items = [.. cart.Items.Select(item => new CreateCartItemResult
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity
            })]
        };
    }
}
