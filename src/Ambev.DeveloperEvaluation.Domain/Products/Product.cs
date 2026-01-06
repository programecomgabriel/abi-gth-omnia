using Ambev.DeveloperEvaluation.Domain.Common.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Products;

/// <summary>
/// Represents a products in the system.
/// </summary>
public class Product : BaseEntity
{
    /// <summary>
    /// Product's title.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Product's price.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Product's description.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Product's category.
    /// </summary>
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// Product's image url.
    /// </summary>
    public string Image { get; set; } = string.Empty;

    /// <summary>
    /// Product's quantity
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets the date and time when the product was created.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets the date and time of the last update to the product's information.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets the product's rating.
    /// </summary>
    public ProductRating Rating { get; set; } = new();

    /// <summary>
    /// Change quantity amount.
    /// </summary>
    /// <param name="quantity">Quantity to change.</param>
    /// <exception cref="DomainException"></exception>
    public void SetQuantity(int quantity)
    {
        if (quantity < 0)
        {
            throw new DomainException("Quantity must not be negative value to the change quantity.");
        }

        Quantity = quantity;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Increase the product quantity.
    /// </summary>
    /// <param name="quantity">Quantity to increase</param>
    /// <exception cref="DomainException"></exception>
    public void IncreaseQuantity(int quantity)
    {
        if (quantity <= 0)
        {
            throw new DomainException("Quantity must be positive value to the increase quantity.");
        }

        Quantity += quantity;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Decrease the product quantity.
    /// </summary>
    /// <param name="quantity">Quantity to reduce</param>
    /// <exception cref="DomainException"></exception>
    public void DecreaseQuantity(int quantity)
    {
        if (quantity <= 0)
        {
            throw new DomainException("Quantity must be positive value to the decrease quantity.");
        }

        Quantity -= quantity;

        if (Quantity < 0)
        {
            throw new DomainException($"Stock quantity must not be negative in product '{Title}'.");
        }

        UpdatedAt = DateTime.UtcNow;
    }
}

public class ProductRating
{
    public decimal Rate { get; set; }
    public int Count { get; set; }
}
