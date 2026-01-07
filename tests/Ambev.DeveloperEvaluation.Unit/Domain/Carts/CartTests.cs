using Ambev.DeveloperEvaluation.Unit.Domain.Carts.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Carts;

/// <summary>
/// Contains unit tests for the Cart entity class.
/// Tests cover cart item creation and amount calculation.
/// </summary>
public class CartTests
{
    [Fact(DisplayName = "New Cart must have single Item and TotalAmount without any discounts")]
    public void Cart_NewCart_Must_Have_Single_Item_Without_Discounts()
    {
        // Arrange
        var cart = CartTestData.GenerateValidCart();
        var cartItem = cart.Items.First();

        // Act
        cartItem.ChangeQuantity(3);
        cart.RefreshTotalAmount();

        // Assert
        Assert.Single(cart.Items);
        Assert.Equal(cartItem.Quantity * cartItem.UnitPrice, cart.TotalAmount);
    }

    [Fact(DisplayName = "New Cart must have single Item and TotalAmount with 10% discounts")]
    public void Cart_NewCart_Must_Have_Single_Item_With_10_Discounts()
    {
        // Arrange
        var cart = CartTestData.GenerateValidCart();
        var cartItem = cart.Items.First();

        // Act
        cartItem.ChangeQuantity(4);
        cart.RefreshTotalAmount();

        // Assert
        Assert.Single(cart.Items);

        var subtotal = cartItem.Quantity * cartItem.UnitPrice;
        var discountAmount = subtotal * (10m / 100m);

        Assert.Equal(subtotal - discountAmount, cart.TotalAmount);
    }

    [Fact(DisplayName = "New Cart must have single Item and TotalAmount with 20% discounts")]
    public void Cart_NewCart_Must_Have_Single_Item_With_20_Discounts()
    {
        // Arrange
        var cart = CartTestData.GenerateValidCart();
        var cartItem = cart.Items.First();

        // Act
        cartItem.ChangeQuantity(12);
        cart.RefreshTotalAmount();

        // Assert
        Assert.Single(cart.Items);

        var subtotal = cartItem.Quantity * cartItem.UnitPrice;
        var discountAmount = subtotal * (20m / 100m);

        Assert.Equal(subtotal - discountAmount, cart.TotalAmount);
    }

    [Fact(DisplayName = "New Cart must throw domain exception when change quantity is above 20")]
    public void Cart_NewCart_Must_Throw_When_Change_Quantity_Above20()
    {
        // Arrange
        var cart = CartTestData.GenerateValidCart();
        var cartItem = cart.Items.First();

        // Act and Assert
        Assert.Throws<DomainException>(() => cartItem.ChangeQuantity(21));
    }

    [Fact(DisplayName = "Cart must cancel single Item and cancel current Cart")]
    public void Cart_Must_Cancel_Single_Item_And_Cancel_Cart()
    {
        // Arrange
        var cart = CartTestData.GenerateValidCart();
        var cartItem = cart.Items.First();

        // Act
        cart.CancelItem(cartItem);

        // Assert
        Assert.True(cart.Cancelled);
    }

    [Fact(DisplayName = "Cart must finalize multiple Items and decrease Product quantity")]
    public void Cart_Must_Finalize_Multiple_Items_And_Decrease_Product_Quantity()
    {
        // Arrange
        var cart = CartTestData.GenerateValidCart();

        cart.AddItems(
            CartTestData.GenerateValidCartItem(),
            CartTestData.GenerateValidCartItem(),
            CartTestData.GenerateValidCartItem()
        );

        var products = cart.Items.Select(ci => new
        {
            ci.Product.Id,
            ci.Product.Quantity
        }).ToList();

        const string saleNumber = "SALE12345";

        // Act
        cart.FinalizeCart(saleNumber);

        // Assert
        Assert.Equal(4, cart.Items.Count);
        Assert.Equal(saleNumber, cart.SaleNumber);
        Assert.True(cart.Sold);

        foreach (var item in cart.Items)
        {
            Assert.Equal(item.Product.Quantity, products.First(p => p.Id == item.ProductId).Quantity - item.Quantity);
        }
    }
}
