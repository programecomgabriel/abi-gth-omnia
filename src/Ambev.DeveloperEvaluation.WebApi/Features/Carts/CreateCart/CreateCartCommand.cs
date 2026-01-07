using MediatR;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart;

/// <summary>
/// Command for creating a new Cart.
/// </summary>
public class CreateCartCommand : IRequest<CreateCartResult>
{
    /// <summary>
    /// Identifier of Product.
    /// </summary>
    public Guid? ProductId { get; set; }

    /// <summary>
    /// Quantity of products.
    /// </summary>
    public int? Quantity { get; set; }

    /// <summary>
    /// Branch where the sale was made.
    /// </summary>
    public string Branch { get; set; } = default!;
}
