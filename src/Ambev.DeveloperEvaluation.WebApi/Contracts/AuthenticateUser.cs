namespace Ambev.DeveloperEvaluation.WebApi.Contracts;

/// <summary>
/// Represents the authentication request model for user login.
/// </summary>
public class AuthenticateUserRequest
{
    /// <summary>
    /// Gets or sets the user's email address for authentication.
    /// Must be a valid email format.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the user's password for authentication.
    /// Must match the stored password after hashing.
    /// </summary>
    public string Password { get; set; } = string.Empty;
}

/// <summary>
/// Represents the response returned after user authentication
/// </summary>
public class AuthenticateUserResponse
{
    /// <summary>
    /// Gets or sets the JWT token for authenticated user
    /// </summary>
    public string Token { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the user's email address
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the user's full name
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the user's role in the system
    /// </summary>
    public string Role { get; set; } = string.Empty;
}
