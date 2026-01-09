namespace Ambev.DeveloperEvaluation.Application.Features.Carts.CreateCart;

/// <summary>
/// Result model for create Cart feature.
/// </summary>
public class CreateCartResult
{
    /// <summary>
    /// The unique identifier of the Cart.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The branch where the sale was made.
    /// </summary>
    public string Branch { get; set; } = default!;

    /// <summary>
    /// Owner of the cart.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Items of the cart.
    /// </summary>
    public List<CreateCartItemResult> Items { get; set; } = default!;
}

/// <summary>
/// Result model for create Cart Item.
/// </summary>
public class CreateCartItemResult
{
    /// <summary>
    /// Identifier of Product.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Quantity of products.
    /// </summary>
    public int Quantity { get; set; }
}
