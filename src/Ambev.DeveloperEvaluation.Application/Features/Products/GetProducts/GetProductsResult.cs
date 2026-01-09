using Ambev.DeveloperEvaluation.Domain.Products;

namespace Ambev.DeveloperEvaluation.Application.Features.Products.GetProducts;

/// <summary>
/// Command result for GetProducts operation.
/// </summary>
public class GetProductsResult
{
    /// <summary>
    /// The unique identifier of the product.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The product's name.
    /// </summary>
    public string Title { get; set; } = default!;

    /// <summary>
    /// Gets the product's description.
    /// </summary>
    public string Description { get; set; } = default!;

    /// <summary>
    /// Gets the product's category.
    /// </summary>
    public string Category { get; set; } = default!;

    /// <summary>
    /// The product's price.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Gets the product's cover image.
    /// </summary>
    public string Image { get; set; } = default!;

    /// <summary>
    /// Gets the product's rating.
    /// </summary>
    public ProductRating Rating { get; set; } = default!;
}
