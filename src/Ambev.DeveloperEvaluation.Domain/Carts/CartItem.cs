using Ambev.DeveloperEvaluation.Domain.Common.Entities;
using Ambev.DeveloperEvaluation.Domain.Products;

namespace Ambev.DeveloperEvaluation.Domain.Carts;

/// <summary>
/// Represents a item of cart.
/// </summary>
public class CartItem : BaseEntity
{
    /// <summary>
    /// Gets the quantity of products.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets the unit price of product.
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Gets the total amount.
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Gets the percentage discount applied to the current item.
    /// </summary>
    public decimal DiscountPercentage { get; set; }

    /// <summary>
    /// Gets the total discount amount applied to the current item.
    /// </summary>
    public decimal DiscountAmount { get; set; }

    /// <summary>
    /// Gets the date and time when the category was created.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets the date and time of the last update to the category's information.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets the date and time when was cancelled the cart.
    /// </summary>
    public bool Cancelled { get; set; }

    /// <summary>
    /// Gets the date and time when was cancelled the cart.
    /// </summary>
    public DateTime? CancelledAt { get; set; }

    /// <summary>
    /// Gets identifier of Cart.
    /// </summary>
    public Guid CartId { get; set; }

    /// <summary>
    /// Gets Cart itself.
    /// </summary>
    public virtual Cart Cart { get; set; } = default!;

    /// <summary>
    /// Gets identifier of Product.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Gets Product itself.
    /// </summary>
    public virtual Product Product { get; set; } = default!;

    /// <summary>
    /// Gets the total amount.
    /// </summary>
    public decimal TotalAmountWithDisccounts
    {
        get
        {
            return TotalAmount - DiscountAmount;
        }
    }

    /// <summary>
    /// Initializes a new instance of the CartItem.
    /// </summary>
    public CartItem()
    {
    }

    /// <summary>
    ///  Initializes a new instance of the CartItem.
    /// </summary>
    /// <param name="quantity">Quantity of products</param>
    /// <param name="product">Product of the CartItem</param>
    /// <exception cref="DomainException"></exception>
    public CartItem(Guid productId, decimal price, int quantity)
    {
        if (quantity <= 0)
        {
            throw new DomainException("Quantity must be positive non zero value.");
        }

        if (quantity > 20)
        {
            throw new DomainException("Maximum limit of 20 items per product.");
        }

        ProductId = productId;
        UnitPrice = price;
        Quantity = quantity;

        RefreshAmounts();
    }

    /// <summary>
    /// Cancels the current purchase and updates its status to cancelled.
    /// </summary>
    public void Cancel()
    {
        Cancelled = true;
        CancelledAt = DateTime.UtcNow;

        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Change quantity.
    /// </summary>
    /// <param name="newQuantity">New quantitty to be changed.</param>
    /// <exception cref="DomainException"></exception>
    public void ChangeQuantity(int newQuantity)
    {
        if (newQuantity <= 0)
        {
            throw new DomainException("New quantity must be positive non zero value.");
        }

        if (newQuantity > 20)
        {
            throw new DomainException("Maximum limit of 20 items per product.");
        }

        Quantity = newQuantity;
        UpdatedAt = DateTime.UtcNow;

        RefreshAmounts();
    }

    /// <summary>
    /// Recalculates the discount percentage, discount amount, and total amount based on the current quantity and unit
    /// price according to business rules.
    /// </summary>
    /// <remarks>This method applies discount rules based on the quantity: no discount for fewer than 4 items,
    /// a 10% discount for 4 to 9 items, and a 20% discount for 10 to 20 items. Quantities above 20 are not allowed.
    /// After recalculation, the DiscountPercentage, DiscountAmount, and TotalAmount properties are updated to reflect
    /// the new values.</remarks>
    public void RefreshAmounts()
    {
        // Business Rules for Discounts:
        // - 4+ items: 10% discount
        // - 10-20 items: 20% discount
        // - Below 4 items: no discount allowed

        DiscountPercentage = Quantity switch
        {
            >= 10 and <= 20 => 20m,
            >= 4 => 10m,
            _ => 0m
        };

        var subtotal = Quantity * UnitPrice;
        DiscountAmount = subtotal * (DiscountPercentage / 100m);
        TotalAmount = subtotal - DiscountAmount;
    }

    /// <summary>
    /// Finalizes the cart item by updating the product's inventory to reflect the quantity purchased.
    /// </summary>
    /// <remarks>Call this method after confirming the purchase to ensure the product's available quantity is
    /// reduced accordingly. This method should only be called once per finalized cart item to prevent incorrect
    /// inventory adjustments.</remarks>
    public void FinalizeCartItem()
    {
        Product.DecreaseQuantity(Quantity);
    }
}
