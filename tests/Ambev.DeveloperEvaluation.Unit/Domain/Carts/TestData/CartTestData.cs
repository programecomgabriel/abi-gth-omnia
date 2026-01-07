using Ambev.DeveloperEvaluation.Domain.Carts;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Ambev.DeveloperEvaluation.Unit.Domain.Products.TestData;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Carts.TestData;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class CartTestData
{
    /// <summary>
    /// Configures the Faker to generate valid Cart.
    /// </summary>
    private static readonly Faker<Cart> CartFaker = new Faker<Cart>()
        .RuleFor(c => c.Id, f => f.Random.Guid())
        .RuleFor(c => c.Branch, f => f.Company.CompanyName())
        .RuleFor(c => c.User, _ => UserTestData.GenerateValidUser())
        .RuleFor(c => c.UserId, (_, c) => c.User.Id)
        .RuleFor(c => c.Items, _ => [CartTestData.GenerateValidCartItem()]);

    /// <summary>
    /// Configures the Faker to generate valid Sold Cart.
    /// </summary>
    private static readonly Faker<Cart> SoldCartFaker = new Faker<Cart>()
        .RuleFor(c => c.Id, f => f.Random.Guid())
        .RuleFor(c => c.SaleNumber, f => f.Random.Long(min: 100000000, max: 999999999).ToString())
        .RuleFor(c => c.SoldAt, f => f.Date.Between(DateTime.UtcNow.AddHours(-1), DateTime.UtcNow.AddHours(1)))
        .RuleFor(c => c.Branch, f => f.Company.CompanyName())
        .RuleFor(c => c.User, _ => UserTestData.GenerateValidUser())
        .RuleFor(c => c.UserId, (_, c) => c.User.Id);

    /// <summary>
    /// Configures the Faker to generate valid CartItem.
    /// </summary>
    private static readonly Faker<CartItem> CartItemFaker = new Faker<CartItem>()
        .RuleFor(c => c.Id, f => f.Random.Guid())
        .RuleFor(c => c.Quantity, f => f.Random.Int(min: 2, max: 20))
        .RuleFor(c => c.Product, _ => ProductTestData.GenerateValidProduct())
        .RuleFor(c => c.UnitPrice, (_, ci) => ci.Product.Price)
        .RuleFor(c => c.ProductId, (_, ci) => ci.Product.Id);

    /// <summary>
    /// Generates a valid Cart entity with randomized data.
    /// The generated Cart will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Cart entity with randomly generated data.</returns>
    public static Cart GenerateValidCart()
    {
        return CartFaker.Generate();
    }

    /// <summary>
    /// Generates a valid Sold Cart entity with randomized data.
    /// The generated Cart will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Sold Cart entity with randomly generated data.</returns>
    public static Cart GenerateValidSoldCart()
    {
        return SoldCartFaker.Generate();
    }

    /// <summary>
    /// Generates a valid CartItem entity with randomized data.
    /// The generated CartItem will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid CartItem entity with randomly generated data.</returns>
    public static CartItem GenerateValidCartItem()
    {
        return CartItemFaker.Generate();
    }
}
