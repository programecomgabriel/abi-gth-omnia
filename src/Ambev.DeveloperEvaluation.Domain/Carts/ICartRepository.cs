namespace Ambev.DeveloperEvaluation.Domain.Carts;

public interface ICartRepository
{
    /// <summary>
    /// Retrieves a cart by id.
    /// </summary>
    /// <param name="id">The unique identifier of the cart</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The cart if found, null otherwise</returns>
    Task<Cart?> GetByIdAsync(Guid id, Guid userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new cart.
    /// </summary>
    /// <param name="cart">The cart to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The cart itself</returns>
    Task<Cart> CreateAsync(Cart cart, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing cart.
    /// </summary>
    /// <param name="cart">The cart to update</param>
    /// <param name="cancellationToken">A cancellation token</param>
    /// <returns>The cart itself.</returns>
    Task<Cart> UpdateAsync(Cart cart, CancellationToken cancellationToken);
}
