using Ambev.DeveloperEvaluation.Common.Security;
using System.Security.Claims;

namespace Ambev.DeveloperEvaluation.WebApi.Common;

public class Session(IHttpContextAccessor contextAccessor)
{
    private SessionUser? _currentSessionUser = null;

    public IUser GetUser()
    {
        if (_currentSessionUser is not null)
        {
            return _currentSessionUser;
        }

        var loggedUser = contextAccessor.HttpContext?.User ?? throw new InvalidOperationException("Unauthorized.");

        var userId = loggedUser.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        var username = loggedUser.FindFirst(ClaimTypes.Name)?.Value ?? string.Empty;
        var userEmail = loggedUser.FindFirst(ClaimTypes.Email)?.Value ?? string.Empty;
        var userRoleName = loggedUser.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty;

        return _currentSessionUser ??= new()
        {
            Id = userId,
            Username = username,
            Email = userEmail,
            Role = userRoleName,
        };
    }

    private sealed record SessionUser : IUser
    {
        public required string Id { get; init; }
        public required string Username { get; init; }
        public required string Email { get; init; }
        public required string Role { get; init; }
    }
}
