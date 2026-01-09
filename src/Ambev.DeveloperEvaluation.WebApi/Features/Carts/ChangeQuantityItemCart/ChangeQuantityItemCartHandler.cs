using Ambev.DeveloperEvaluation.Domain.Carts;
using Ambev.DeveloperEvaluation.Domain.Products;
using Ambev.DeveloperEvaluation.Domain.Users;
using Ambev.DeveloperEvaluation.WebApi.Common;
using MediatR;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.ChangeQuantityItemCart;

/// <summary>
/// Handler for processing <see cref="ChangeQuantityItemCartCommand"/> requests.
/// </summary>
public class ChangeQuantityItemCartHandler(
    Session session,
    IUserRepository userRepository,
    IProductRepository productRepository,
    ICartRepository cartRepository) : IRequestHandler<ChangeQuantityItemCartCommand, ChangeQuantityItemCartResult>
{
    /// <summary>
    /// Handles the ChangeQuantityItemCartCommand request
    /// </summary>
    /// <param name="command">The ChangeQuantityItemCart command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created cart details</returns>
    public async Task<ChangeQuantityItemCartResult> Handle(ChangeQuantityItemCartCommand command, CancellationToken cancellationToken)
    {
        var sessionUser = session.GetUser();

        if (!Guid.TryParse(sessionUser.Id, out var userId))
        {
            throw new InvalidOperationException("Invalid user ID in session.");
        }

        var user = await userRepository.GetByIdAsync(userId, cancellationToken) ?? throw new InvalidOperationException("User not found.");

        var cart = await cartRepository.GetByIdAsync(command.Id!.Value, user.Id, cancellationToken) ?? throw new InvalidOperationException("Cart not found.");

        var product = await productRepository.GetByIdAsync(command.ProductId!.Value, cancellationToken) ?? throw new InvalidOperationException("Product not found.");

        var cartItem = cart.GetItem(product.Id);
        cartItem.ChangeQuantity(command.Quantity!.Value);

        await cartRepository.UpdateAsync(cart, cancellationToken);

        return new ChangeQuantityItemCartResult
        {
            Id = cart.Id,
            ProductId = cartItem.ProductId,
            Quantity = cartItem.Quantity
        };
    }
}
