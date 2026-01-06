using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Auth.AuthenticateUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Auth;

/// <summary>
/// Controller for authentication operations
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AuthController : BaseController
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Initializes a new instance of AuthController
    /// </summary>
    /// <param name="mediator">The mediator instance</param>
    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Authenticates a user with their credentials
    /// </summary>
    /// <param name="request">The authentication request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Authentication token if successful</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<AuthenticateUserResult>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> AuthenticateUser([FromBody] AuthenticateUserCommand command, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(command, cancellationToken);

        return Ok(new ApiResponseWithData<AuthenticateUserResult>
        {
            Success = true,
            Message = "User authenticated successfully",
            Data = response
        });
    }
}
