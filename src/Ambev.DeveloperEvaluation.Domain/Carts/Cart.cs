using Ambev.DeveloperEvaluation.Domain.Common.Entities;
using Ambev.DeveloperEvaluation.Domain.Users;

namespace Ambev.DeveloperEvaluation.Domain.Carts;

/// <summary>
/// Represents a cart with itens.
/// </summary>
public class Cart : BaseEntity
{
    /// <summary>
    /// Gets the sale number.
    /// </summary>
    public string SaleNumber { get; set; } = string.Empty;

    /// <summary>
    /// Gets the date when the sale was made.
    /// </summary>
    public DateTime SoldAt { get; set; }

    /// <summary>
    /// Gets the total a sale amount.
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Gets the branch where the sale was made.
    /// </summary>
    public string Branch { get; set; } = string.Empty;

    /// <summary>
    /// Gets the date and time when the cart was created.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets the date and time when was updated the cart.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets if cart was cancelled.
    /// </summary>
    public bool Cancelled { get; set; }

    /// <summary>
    /// Gets the date and time when was cancelled the cart.
    /// </summary>
    public DateTime? CancelledAt { get; set; }

    /// <summary>
    /// Gets if cart was cancelled.
    /// </summary>
    public bool Sold { get; set; }

    /// <summary>
    /// Gets a owner of the cart.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets a owner of the cart.
    /// </summary>
    public virtual User User { get; set; } = new();

    /// <summary>
    /// Gets items of the cart.
    /// </summary>
    public virtual ICollection<CartItem> Items { get; set; } = [];

    /// <summary>
    /// Initializes a new instance of the Cart.
    /// </summary>
    public Cart()
    {
    }

    /// <summary>
    /// Initializes a new instance of the Cart.
    /// </summary>
    /// <param name="branch">Branch where the sale was made.</param>
    /// <param name="user">Owner of the cart.</param>
    /// <param name="items">Items of the cart.</param>
    public Cart(string branch, User user, params CartItem[] items)
    {
        ArgumentNullException.ThrowIfNull(items);

        Branch = branch;
        User = user;
        Items = items;
    }

    /// <summary>
    /// Adding new item and quantity to the cart.
    /// </summary>
    /// <param name="items">Adding items of cart.</param>
    public void AddItems(params CartItem[] items)
    {
        ArgumentNullException.ThrowIfNull(items);

        foreach (var item in items)
        {
            Items.Add(item);
        }

        RefreshTotalAmount();
    }

    /// <summary>
    /// Cancel the cart.
    /// </summary>
    public void Cancel()
    {
        if (Cancelled) return;

        Cancelled = true;
        CancelledAt = DateTime.UtcNow;

        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Cancel the cart.
    /// </summary>
    /// <param name="item">Item to cancel</param>
    public void CancelItem(CartItem item)
    {
        ArgumentNullException.ThrowIfNull(item);

        if (item.Cancelled) return;

        item.Cancel();

        if (Items.All(i => i.Cancelled))
        {
            Cancelled = true;
            CancelledAt = DateTime.UtcNow;
        }

        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Change quantity of item.
    /// </summary>
    /// <param name="items">Items to update the quantity.</param>
    public void ChangeQuantity(CartItem item, int newQuantity)
    {
        item.ChangeQuantity(newQuantity);

        RefreshTotalAmount();
    }

    /// <summary>
    /// Recalculates and updates the total amount based on the current items in the collection.
    /// </summary>
    /// <remarks>Call this method after modifying the items to ensure that the total amount reflects the
    /// latest values. This method does not return a value; it updates the TotalAmount property.</remarks>
    public void RefreshTotalAmount()
    {
        TotalAmount = Items.Sum(i => i.TotalAmount);
    }

    /// <summary>
    /// Finalizes the cart by marking it as sold and finalizing all items within the cart.
    /// </summary>
    /// <remarks>This method has no effect if the cart has already been marked as sold. After calling this
    /// method, the cart and its items are considered finalized and cannot be modified.</remarks>
    /// <param name="saleNumber">The unique identifier to assign to the finalized sale. Cannot be null.</param>
    public void FinalizeCart(string saleNumber)
    {
        ArgumentNullException.ThrowIfNull(saleNumber);

        if (Sold) return;

        SaleNumber = saleNumber;
        Sold = true;

        foreach (var item in Items)
        {
            item.FinalizeCartItem();
        }
    }
}
