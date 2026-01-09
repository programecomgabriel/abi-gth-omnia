using Ambev.DeveloperEvaluation.Domain.Users;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;
using Bogus;

namespace Ambev.DeveloperEvaluation.Integration.WebApi.Features.Users.TestData;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class CreateUserCommandTestData
{
    /// <summary>
    /// Configures the Faker to generate valid CreateUserCommand.
    /// </summary>
    private static readonly Faker<CreateUserCommand> CreateUserCommandFaker = new Faker<CreateUserCommand>()
        .RuleFor(u => u.Username, f => f.Internet.UserName())
        .RuleFor(u => u.Password, f => $"Test@{f.Random.Number(100, 999)}")
        .RuleFor(u => u.Email, f => f.Internet.Email())
        .RuleFor(u => u.Phone, f => $"+55{f.Random.Number(11, 99)}{f.Random.Number(100000000, 999999999)}")
        .RuleFor(u => u.Status, f => f.PickRandom(UserStatus.Active, UserStatus.Suspended))
        .RuleFor(u => u.Role, f => f.PickRandom(UserRole.Customer, UserRole.Admin));

    /// <summary>
    /// Generates a valid CreateUserCommand entity with randomized data.
    /// The generated CreateUserCommand will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid CreateUserCommand entity with randomly generated data.</returns>
    public static CreateUserCommand GenerateValidCreateUserCommand()
    {
        return CreateUserCommandFaker.Generate();
    }
}
