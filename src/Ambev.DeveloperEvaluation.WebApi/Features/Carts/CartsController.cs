using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts;

/// <summary>
/// Controller for managing Cart operations
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CartsController(IMediator mediator) : BaseController
{
    /// <summary>
    /// Creates a new cart
    /// </summary>
    /// <param name="command">The cart creation command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created cart details</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateCartResult>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCart([FromBody] CreateCartCommand command, CancellationToken cancellationToken)
    {
        var response = await mediator.Send(command, cancellationToken);

        return Created(string.Empty, new ApiResponseWithData<CreateCartResult>
        {
            Success = true,
            Message = "Cart created successfully",
            Data = response
        });
    }
}
