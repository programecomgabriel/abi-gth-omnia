namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.ChangeQuantityItemCart;

/// <summary>
/// Request for change quantity product of the Cart.
/// </summary>
public class ChangeQuantityItemCartRequest
{
    /// <summary>
    /// The unique identifier of the Cart.
    /// </summary>
    public Guid? Id { get; set; }

    /// <summary>
    /// Identifier of Product.
    /// </summary>
    public Guid? ProductId { get; set; }

    /// <summary>
    /// Quantity of products.
    /// </summary>
    public int? Quantity { get; set; }
}
