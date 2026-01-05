namespace Ambev.DeveloperEvaluation.WebApi.Contracts.Users;

/// <summary>
/// Request model for deleting a user
/// </summary>
public class DeleteUserRequest
{
    /// <summary>
    /// The unique identifier of the user to delete
    /// </summary>
    public Guid Id { get; set; }
}

/// <summary>
/// Response model for DeleteUser operation
/// </summary>
public class DeleteUserResponse
{
    /// <summary>
    /// Indicates whether the deletion was successful
    /// </summary>
    public bool Success { get; set; }
}
