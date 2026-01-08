using Ambev.DeveloperEvaluation.Domain.Products;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;
using Bogus;

namespace Ambev.DeveloperEvaluation.Integration.WebApi.Features.Products.TestData;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class CreateProductCommandTestData
{
    /// <summary>
    /// Configures the Faker to generate valid CreateProductCommand.
    /// </summary>
    private static readonly Faker<CreateProductCommand> CreateProductCommandFaker = new Faker<CreateProductCommand>()
        .RuleFor(p => p.Title, f => f.Commerce.ProductName())
        .RuleFor(p => p.Price, f => f.Random.Decimal(min: 1, max: 2000.00M))
        .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
        .RuleFor(p => p.Category, f => f.Commerce.Department(f.Random.Int(1, 3)))
        .RuleFor(p => p.Image, f => f.Internet.Url())
        .RuleFor(p => p.Quantity, f => f.Random.Int(min: 200, max: 1000))
        .RuleFor(p => p.Rating, f => new ProductRating
        {
            Rate = f.Random.Decimal(min: 1, max: 5),
            Count = f.Random.Int(min: 1, max: 1000)
        });

    /// <summary>
    /// Generates a valid CreateProductCommand entity with randomized data.
    /// The generated CreateProductCommand will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid CreateProductCommand entity with randomly generated data.</returns>
    public static CreateProductCommand GenerateValidProductCommand()
    {
        return CreateProductCommandFaker.Generate();
    }
}
