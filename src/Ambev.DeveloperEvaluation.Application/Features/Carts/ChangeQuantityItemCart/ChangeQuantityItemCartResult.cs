namespace Ambev.DeveloperEvaluation.Application.Features.Carts.ChangeQuantityItemCart;

/// <summary>
/// Result model for change quantity CartItem feature.
/// </summary>
public class ChangeQuantityItemCartResult
{
    /// <summary>
    /// The unique identifier of the Cart.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Identifier of Product.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Quantity of products.
    /// </summary>
    public int Quantity { get; set; }
}
