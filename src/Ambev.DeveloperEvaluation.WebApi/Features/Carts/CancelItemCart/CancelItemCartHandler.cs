using Ambev.DeveloperEvaluation.Domain.Carts;
using Ambev.DeveloperEvaluation.Domain.Products;
using Ambev.DeveloperEvaluation.Domain.Users;
using Ambev.DeveloperEvaluation.WebApi.Common;
using MediatR;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CancelItemCart;

/// <summary>
/// Handler for processing <see cref="CancelItemCartCommand"/> requests.
/// </summary>
public class CancelItemCartHandler(
    Session session,
    IUserRepository userRepository,
    IProductRepository productRepository,
    ICartRepository cartRepository) : IRequestHandler<CancelItemCartCommand, CancelItemCartResult>
{
    /// <summary>
    /// Handles the ChangeQuantityItemCartCommand request
    /// </summary>
    /// <param name="command">The ChangeQuantityItemCart command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The cancelled cart item details</returns>
    public async Task<CancelItemCartResult> Handle(CancelItemCartCommand command, CancellationToken cancellationToken)
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

        cart.CancelItem(cartItem);

        await cartRepository.UpdateAsync(cart, cancellationToken);

        return new CancelItemCartResult
        {
            Id = cart.Id,
            ProductId = cartItem.ProductId
        };
    }
}
