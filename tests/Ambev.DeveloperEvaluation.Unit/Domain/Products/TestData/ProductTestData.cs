using Ambev.DeveloperEvaluation.Domain.Products;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Products.TestData;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class ProductTestData
{
    /// <summary>
    /// Configures the Faker to generate valid Product.
    /// </summary>
    private static readonly Faker<Product> ProductFaker = new Faker<Product>()
        .RuleFor(p => p.Id, f => f.Random.Guid())
        .RuleFor(p => p.Title, f => f.Commerce.ProductName())
        .RuleFor(p => p.Price, f => f.Random.Decimal(min: 1, max: 2000.00M))
        .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
        .RuleFor(p => p.Category, f => f.Commerce.Department(f.Random.Int(1, 3)))
        .RuleFor(p => p.Image, f => f.Internet.Url())
        .RuleFor(p => p.Quantity, f => f.Random.Int(min: 200, max: 1000));

    /// <summary>
    /// Generates a valid Product entity with randomized data.
    /// The generated Product will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Product entity with randomly generated data.</returns>
    public static Product GenerateValidProduct()
    {
        return ProductFaker.Generate();
    }
}
