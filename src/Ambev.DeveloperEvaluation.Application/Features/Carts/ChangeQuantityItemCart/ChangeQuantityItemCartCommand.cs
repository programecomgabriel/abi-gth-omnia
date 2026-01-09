using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Features.Carts.ChangeQuantityItemCart;

/// <summary>
/// Command for change quantity product of the Cart.
/// </summary>
public class ChangeQuantityItemCartCommand : IRequest<ChangeQuantityItemCartResult>
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

    /// <summary>
    /// Identifier of User.
    /// </summary>
    public Guid? UserId { get; set; }
}
