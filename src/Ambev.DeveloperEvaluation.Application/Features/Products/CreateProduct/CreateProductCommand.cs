using Ambev.DeveloperEvaluation.Domain.Products;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Features.Products.CreateProduct;

/// <summary>
/// Command for creating a new product.
/// </summary>
public class CreateProductCommand : IRequest<CreateProductResult>
{
    /// <summary>
    /// Gets or sets the name of the product to be created.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets the product's description.
    /// </summary>
    public string Description { get; set; } = default!;

    /// <summary>
    /// Gets the product's category name.
    /// </summary>
    public string Category { get; set; } = default!;

    /// <summary>
    /// Gets or sets the price for the product.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Gets the product's cover image.
    /// </summary>
    public string Image { get; set; } = default!;

    /// <summary>
    /// Gets the stock quantity
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets the product's rating.
    /// </summary>
    public ProductRating Rating { get; set; } = default!;
}
