using Ambev.DeveloperEvaluation.Domain.Products;

namespace Ambev.DeveloperEvaluation.WebApi.Contracts.Products;

/// <summary>
/// Represents a request to create a new product.
/// </summary>
public class CreateProductRequest
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

/// <summary>
/// Represents a response to created product.
/// </summary>
public class CreateProductResponse
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
    /// Gets the product's category name.
    /// </summary>
    public string CategoryName { get; set; } = default!;

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
