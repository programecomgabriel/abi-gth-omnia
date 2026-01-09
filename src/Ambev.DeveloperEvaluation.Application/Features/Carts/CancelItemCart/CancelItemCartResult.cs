namespace Ambev.DeveloperEvaluation.Application.Features.Carts.CancelItemCart;

/// <summary>
/// Result model for cancel CartItem feature.
/// </summary>
public class CancelItemCartResult
{
    /// <summary>
    /// The unique identifier of the Cart.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Identifier of Product.
    /// </summary>
    public Guid ProductId { get; set; }
}
