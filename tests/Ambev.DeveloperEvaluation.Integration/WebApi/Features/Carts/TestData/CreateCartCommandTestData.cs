using Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart;
using Bogus;

namespace Ambev.DeveloperEvaluation.Integration.WebApi.Features.Carts.TestData;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class CreateCartCommandTestData
{
    /// <summary>
    /// Configures the Faker to generate valid CreateCartCommand.
    /// </summary>
    private static readonly Faker<CreateCartCommand> CreateCartCommandFaker = new Faker<CreateCartCommand>()
        .RuleFor(c => c.Branch, f => f.Company.CompanyName());

    /// <summary>
    /// Generates a valid CreateCartCommand entity with randomized data.
    /// The generated CreateCartCommand will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid CreateCartCommand entity with randomly generated data.</returns>
    public static CreateCartCommand GenerateValidCreateCartCommand()
    {
        return CreateCartCommandFaker.Generate();
    }
}
