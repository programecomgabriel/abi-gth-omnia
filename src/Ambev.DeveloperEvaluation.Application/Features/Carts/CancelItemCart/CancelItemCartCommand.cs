using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Features.Carts.CancelItemCart;

/// <summary>
/// Command for cancel an product of the Cart.
/// </summary>
public class CancelItemCartCommand : IRequest<CancelItemCartResult>
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
    /// Identifier of User.
    /// </summary>
    public Guid? UserId { get; set; }
}
